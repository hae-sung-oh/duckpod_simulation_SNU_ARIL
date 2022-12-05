using UnityEngine;

namespace RosSharp.RosBridgeClient {
    public class TwistImporter : UnitySubscriber<MessageTypes.Geometry.Twist> {
        public GameObject UrdfModel;
        private TwistManager_ twistManager;
        private Vector3 linearVelocity;
        private Vector3 angularVelocity;
        private bool isMessageReceived;

        protected override void Start() {
            base.Start();
            twistManager = UrdfModel.GetComponent<TwistManager_>();
        }

        protected override void ReceiveMessage(MessageTypes.Geometry.Twist message) {
            linearVelocity = ToVector3(message.linear).Ros2Unity();
            angularVelocity = -ToVector3(message.angular).Ros2Unity();
            isMessageReceived = true;
        }

        private static Vector3 ToVector3(MessageTypes.Geometry.Vector3 geometryVector3) {
            return new Vector3((float)geometryVector3.x, (float)geometryVector3.y, (float)geometryVector3.z);
        }

        private void Update() {
            if (isMessageReceived)
                twistManager.updateTransform(linearVelocity.z, angularVelocity.y);
        }
        private void ProcessMessage() {
            /*float deltaTime = Time.realtimeSinceStartup - previousRealTime;
            SubscribedTransform.Translate(linearVelocity * deltaTime);
            SubscribedTransform.Rotate(Vector3.forward, angularVelocity.x * deltaTime);
            SubscribedTransform.Rotate(Vector3.up, angularVelocity.y * deltaTime);
            SubscribedTransform.Rotate(Vector3.left, angularVelocity.z * deltaTime);
            isMessageReceived = false;*/
        }
    }
}