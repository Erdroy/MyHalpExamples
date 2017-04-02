using MyHalp;
using UnityEngine;

public class MyObjectPoolExample : MyComponent
{
    public class Bullet : MyComponent
    {
        private const float BulletSpeed = 10.0f; // 10.0 ups
        private const float LifeTime = 5.0f; // one second
        
        private int _poolIndex; // the pool index
        private Vector3 _velocity;
        
        protected override void OnInit()
        {
            Destroy(this, LifeTime);

            _velocity = transform.forward.normalized * BulletSpeed * Time.fixedDeltaTime;
        }

        protected override void OnPhysicsTick()
        {
            // simple hitscan

            if (Physics.Raycast(transform.position, transform.forward, _velocity.magnitude))
            {
                Debug.Log(string.Format("Hit({0})!", _poolIndex));
                Destroy(this);
                return;
            }

            transform.position += _velocity;
        }
        
        protected override void OnDestroy()
        {
            // release the object in the object pool
            MyObjectPool.Release(_poolIndex);
            Debug.Log("Released bullet: " + _poolIndex);
        }

        public static Bullet Spawn(Vector3 position, Vector3 direction)
        {
            // request new object from the object pool
            int index;
            var obj = MyObjectPool.Request(out index);

            if (obj)
            {
                // successfully allocated gameobject

                // set the starting params
                obj.transform.position = position;
                obj.transform.forward = direction;

                // add bullet script to the allocated object
                var bullet = obj.AddComponent<Bullet>();

                // set the pool index to allow release of the allocated object
                bullet._poolIndex = index;

                Debug.Log("Spawned bullet: " + index);

                return bullet;
            }

            // failed to spawn bullet
            return null;
        }
    }
    
    protected override void OnInit()
    {
        // initialize object pool with 10 objects, pretty small - but this is for testing
        MyObjectPool.Init(10);
    }

    protected override void OnTick()
    {
        // simple fire
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!Bullet.Spawn(Camera.main.transform.position, Camera.main.ScreenPointToRay(Input.mousePosition).direction))
            {
                Debug.LogWarning("Failed to spawn bullet!");
            }
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("Click LMP to shoot");
    }
}
