using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Fuzzy;
using Assets.Scripts.Fuzzy.Membership;
using Assets.Scripts.Fuzzy.Operators;
using Assets.Scripts.Fuzzy.Defuzzification;

public class FuzzyTest : MonoBehaviour
{
    void Start()
    {
        Example1();
        Example3();
    }

    void Example1()
    {
        var financiamento = new Variable("financiamento",
            new Set("inadequado", new L(25f, 40f)),
            new Set("reduzido", new Lambda(30f, 55f, 80f)),
            new Set("adequado", new Gamma(65f, 85f))
        );
        var rh = new Variable("rh",
            new Set("pequeno", new L(15f, 65f)),
            new Set("grande", new Gamma(25f, 75f))
        );
        var risco = new Variable("risco",
            new Set("pequeno", new L(25f, 40f)),
            new Set("normal", new Trapezoidal(25f, 45f, 55f, 75f)),
            new Set("alto", new Gamma(60f, 75f))
        );

        financiamento.Value = 70f;
        rh.Value = 30f;

        var pequeno = Mamdami.Or(financiamento["adequado"], rh["pequeno"]);
        var normal = Mamdami.And(financiamento["reduzido"], rh["grande"]);
        var alto = financiamento["inadequado"];

        var inferred = new Dictionary<string, float>() {
            { "pequeno", pequeno},
            { "normal", normal},
            { "alto", alto}
        };

        var samples = new List<float>() { 10f, 20f, 30f, 40f, 50f, 60f, 70f };

        var centroid = new Centroid(risco, samples);
        var defuzzified = centroid.Eval(inferred);
        Debug.Log("defuzzified: Risco = " + defuzzified);
    }

    void Example3()
    {
        var energia = new Variable("energia", new Set("alta", new Gamma(0, 90)));
        var municao = new Variable("municao", new Set("muita", new Gamma(0, 20)));
        var estado = new Variable("estado",
            new Set("fraco", new L(30f, 60f)),
            new Set("forte", new Gamma(30f, 80f))
        );

        energia.Value = 55f;
        municao.Value = 8f;

        var forte = Mamdami.And(energia["alta"], municao["muita"]);
        var fraco = Mamdami.Or(
            Mamdami.Not(energia["alta"]),
            Mamdami.Not(municao["muita"])
        );

        var inferred = new Dictionary<string, float>() {
            { "fraco", fraco },
            { "forte", forte}
        };
        var samples = new List<float>() { 0f, 10f, 20f, 30f, 40f, 50f, 60f, 70f, 80f, 90f, 100f };

        var centroid = new Centroid(estado, samples);
        var defuzzified = centroid.Eval(inferred);
        Debug.Log("defuzzified: Estado = " + defuzzified);
    }
}
