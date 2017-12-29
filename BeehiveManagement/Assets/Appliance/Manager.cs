using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    Appliance[] kitchenWare = new Appliance[2];

    CoffeeMaker misterCoffee = new CoffeeMaker();
    Oven oldToasty = new Oven();


    private void Start()
    {
        kitchenWare[0] = misterCoffee;
        kitchenWare[1] = oldToasty;

        Appliance powerConsumer = new CoffeeMaker();

        powerConsumer.ConsumePower();

        if (powerConsumer is CoffeeMaker)
        {
            CoffeeMaker javeJoe = powerConsumer as CoffeeMaker;

            javeJoe.MakeCoffee();
        }

        Oven misterToasty = new Oven();

        ICooksFood cooker;

        if (misterToasty is ICooksFood)
        {
            cooker = misterToasty as ICooksFood;

            cooker.Reheat();
        }

        Appliance powerConsumerTest;

        if (misterToasty is Oven)
        {
            powerConsumerTest = misterToasty;

            //powerConsumerTest.pluggedIn;
        }
    }



}
