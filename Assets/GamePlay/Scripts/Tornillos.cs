using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornillos : BaseGame
{
    private float _speed = 4f;
    private float _rotationSpeed = 10f;
    private Vector3 _towardsTarget;
    private Vector3 _targetPosition;
    private int _mode = 0;
    public int _Mode
    {
        get => _mode;
        set => _mode = value;
    }
    void Start()
    {
        if(gameObject.tag == GameConstants.APARECER_TORNILLO_TAG)
        {
            var rg = gameObject.AddComponent <Rigidbody>();
            rg.useGravity = false;
            _targetPosition = transform.position + Random.insideUnitSphere * 2f;
            _targetPosition.y = 1;
            _mode = 1;
        }
    }

    void Update()
    {
        if(_mode == 1 || _mode == 2) buscarPunto();
    }

    //Si se detecta un collider, se comprueba el tag; si es tipo 'Player',
    //se elimina el Tornillo (ha sido recolectado)
    public void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == GameConstants.PLAYER_TAG) Destroy(gameObject);
    }

    //Si aparece por muerte de enemigo, buscará un punto aleatorio
    //Si detecta un 'Player', ira a por él
    //Si el tornillo ha llegado al punto aleatorio, se convierte al tag 'Tornillo'
    public void buscarPunto()
    {
        BaseGame.PlayerTargetPosition.y +=1f;
        
        if(_mode == 1) _towardsTarget = _targetPosition - transform.position;
        else if(_mode == 2) _towardsTarget = BaseGame.PlayerTargetPosition - transform.position; 

        suavizarMovimiento(transform,_towardsTarget,_speed, _rotationSpeed);

        if(_mode == 1 && _towardsTarget.magnitude < 0.1f) _mode = 0; gameObject.tag = GameConstants.TORNILLO_TAG;
    }
}