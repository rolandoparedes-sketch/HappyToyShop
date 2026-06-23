


public interface IState
{

    
    void Enter();

    //-> Se llama CADA FRAME  mientras el estado esta activo
    void Update();


//-> Se llama UNA VEZ cuando el estado esta apunto de cambiar
    void Exit();

 

}

