using UnityEngine;

public class HanabiScatterer_fade : HanabiScatterer
{
    [SerializeField] private float firstSize;
    [SerializeField] private float lastSize;
    [SerializeField] private float rot;

    public override void SetParameter(HanabiParticle particle, Vector2 velocity, float multiply)
    {
        particle.SetMovement(velocity * multiply, velocity * multiply, 0f);
        particle.transform.Rotate(Vector3.forward,rot);
        particle.SetSize(firstSize, lastSize);
        particle.SetFadeout();
    }
}
