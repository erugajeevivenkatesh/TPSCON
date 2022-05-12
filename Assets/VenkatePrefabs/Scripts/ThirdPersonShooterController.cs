using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;

public class ThirdPersonShooterController : MonoBehaviour
{
   [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimsensitivity;
    [SerializeField] private LayerMask aimcolliderMask=new LayerMask();
    [SerializeField] private Transform PfBulletProjectile;
    [SerializeField] private Transform SpawnBullletPosiion;

    private StarterAssetsInputs startedAssetInputs;
    private ThirdPersonController thirdPersonController;

    [SerializeField] private Transform VxHitGreen;
    [SerializeField] private Transform VxHitRed;

    [SerializeField] private Transform debugTransform;

    private Animator m_aniamator;

    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        startedAssetInputs = GetComponent<StarterAssetsInputs>();  
        m_aniamator = GetComponent<Animator>();
        
    }
    private void Update()
    {
        Vector2 screenCenterpoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Vector3 mouserworld_Pos = Vector3.zero;
        Transform hittransfom = null;

        Ray ray = Camera.main.ScreenPointToRay(screenCenterpoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimcolliderMask))
        {
            debugTransform.position = raycastHit.point;
            mouserworld_Pos = raycastHit.point;
            hittransfom = raycastHit.transform;
        }
        //
        if (startedAssetInputs.aim)
        {
            thirdPersonController.SetRotationMove(false);
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.setSensitivy(aimsensitivity);
            m_aniamator.SetLayerWeight(1, Mathf.Lerp(m_aniamator.GetLayerWeight(1),1f,Time.deltaTime*10f));
            
            Vector3 worldAimTarget = mouserworld_Pos;
            worldAimTarget.y=transform.position.y;
            Vector3 aimdirection = (worldAimTarget - transform.position).normalized;


            transform.forward = Vector3.Lerp(transform.forward, aimdirection, Time.deltaTime * 20f);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.setSensitivy(normalSensitivity);
            thirdPersonController.SetRotationMove(true);
            m_aniamator.SetLayerWeight(1, Mathf.Lerp(m_aniamator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));


        }
        if (startedAssetInputs.shoot)
        {
            if(hittransfom!=null)
            {
                if (hittransfom.GetComponent<BulleTarget>() != null)
                {
                    Instantiate(VxHitGreen, debugTransform.position, Quaternion.identity);
                    //Hit Target
                }
                else
                {
                    Instantiate(VxHitRed, debugTransform.position, Quaternion.identity);
                    // hit Something Else
                }
            }
           // Vector3 aimdir = (mouserworld_Pos - SpawnBullletPosiion.position).normalized;
           // Instantiate(PfBulletProjectile,SpawnBullletPosiion.position,Quaternion.LookRotation(aimdir,Vector3.up));
          startedAssetInputs.shoot = false;
        }

      
    }
}
