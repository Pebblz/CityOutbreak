using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float force = 5f;
    public float direction = 1f;
    private bool thrown;
    public float Timer = 1f;
    void Start()
    {
        thrown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!thrown)
        {
            float forceY = force * Mathf.Sqrt(3f);
            GetComponent<Rigidbody>().AddForce(new Vector3(force * direction, forceY, 0), ForceMode.Impulse);
            thrown = true;
        }

        if (Timer <= 0)
        {
            Destroy(this.gameObject);
        }
        Timer -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name != "LobParticle(Clone)" && col.gameObject.tag != "Enemy")
        {
            Destroy(this.gameObject);
        }
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Player>().Take3Damage();
            Destroy(this.gameObject);

        }
    }

}
