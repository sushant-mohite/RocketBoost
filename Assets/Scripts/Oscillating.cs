using UnityEngine;

public class Oscillating : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period == 0) {return;}                     // because any number cannot be devided by zero, thats why we got NAN error
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;                // constatnt value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau);      // rawSinWave = in between -1 - 1.

        movementFactor = (rawSinWave + 1f) / 2f;          // +1 makes it 0 - 2 but we want 0 - 1, so devide by 2 makes it 0 - 1.
        
        Vector3 offset = movementVector * movementFactor;   // (30,0,0) * (0,1)
        transform.position = startingPosition + offset;
    }
}
