using UnityEngine;
using System.Collections;

namespace EntroMinds.NetworkScript
{
    public class NetworkCharacter : Photon.MonoBehaviour
    {
        private Vector3 correctPlayerPos = Vector3.zero;
        private Quaternion correctPlayerRot = Quaternion.identity;

        // Update is called once per frame
        void Update()
        {
            if(!photonView.isMine)
            {
                transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * 5f);
                transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * 5f);
            }
        }

        void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if(stream.isWriting)
            {
                // Local Player Code...Send location/rotation to network
                stream.SendNext(transform.position);
                stream.SendNext(transform.rotation);
            }
            else
            {
                // Network Player Code...receive location/rotation from network
                correctPlayerPos = (Vector3)stream.ReceiveNext();
                correctPlayerRot = (Quaternion)stream.ReceiveNext();
            }
        }
    }
}