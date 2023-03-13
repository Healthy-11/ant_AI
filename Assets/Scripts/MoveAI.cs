using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAI : MonoBehaviour
{ //Variables
    public GameObject aMiel1;
    public GameObject aMiel2;
    public float smellRadius = 10;
    public int speedMin;
    public int speedMax;
    public int speedRushMielMin;
    public int speedRushMielMax;
    public int turnChanceMax;
    public Vector3 direction;
    readonly System.Random rnd = new System.Random();
    private int speedRan;
    private int turnChance;
    private bool testAMiel = false;
    public List<int> transportMiel = new List<int>();
    private Vector3 centerOfSphere;
    public Collider[] collidersInside;
    public List<Vector3> Mielpos = new List<Vector3>();

    void Start() //AU LANCEMENT
    {
        aMiel1.SetActive(false); //cacher les ports de miel
        aMiel2.SetActive(false);
        direction = (new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f))).normalized; //direction aléatoire
        speedRan = rnd.Next(speedMin, speedMax); //vitesse aléatoire
        
    }


    void Update() //A CHAQUE FRAME DU JEU
    {
        centerOfSphere = gameObject.transform.position; //désignation du centre de l'overlapsphere (sensory system)
        collidersInside = Physics.OverlapSphere(centerOfSphere, smellRadius, 1); //application du sensory system
        for (int i = 0; i < collidersInside.Length; i++)
        {
            if (collidersInside[i].tag.Contains("Miel"))
             {
             Mielpos.Add(collidersInside[i].transform.position);
             speedRan = rnd.Next(speedRushMielMin, speedRushMielMax);
             transform.position = Vector3.MoveTowards(transform.position, Mielpos[0], Time.deltaTime * speedRan);   
             }
        }
        turnChance = rnd.Next(1, turnChanceMax); //1 chance sur 500 de partir dans une autre direction aléatoire avec une vitesse aléatoire allant de 5 à 10
        if (turnChance == 1) 
        {
            direction = (new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f))).normalized;        
        }
        transform.position += direction * speedRan * Time.deltaTime;
        if (transportMiel.Count == 1) //si transport de miel, alors l'afficher
        {
            aMiel2.SetActive(true);
        }
        if (transportMiel.Count > 1) //si transport de beaucoup de miel alors l'afficher
        {
            aMiel1.SetActive(true);
        }
        if (Mielpos.Count > 1) //si le Mielpos est supérieur à 1 (plus d'une donnée) alors on enlève toutes les données à part la première
        {
            Mielpos.RemoveRange(1, Mielpos.Count-1); ; 
        }

    }

    IEnumerator OnCollisionEnter(Collision hit) //A CHAQUE COLLISION

    {
        if (hit.gameObject.CompareTag("Miel") & testAMiel == true) //si miel touché alors, il s'arrete, et transporte le miel
        {
            
            speedRan = 0;
            yield return new WaitForSeconds(2);
            transportMiel.Add(1);
            Mielpos.Clear(); //on enlève toutes les Mielpos après avoir récolté le miel, permettant à de nouvelles coords de rentrer
            direction = (new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f))).normalized;
            speedRan = rnd.Next(speedMin, speedMax);
        }
        else if (hit.gameObject.CompareTag("Miel") & testAMiel == false)
        {
            
            speedRan = 0;
            yield return new WaitForSeconds(2);
            testAMiel = true;
            transportMiel.Add(1);
            Mielpos.Clear();//On enlève toutes les Mielpos après avoir récolté le miel, permettant à de nouvelles coords de rentrer
            direction = (new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f))).normalized;
            speedRan = rnd.Next(speedMin, speedMax);
        }
        direction = Vector3.Reflect(direction, hit.contacts[0].normal); //direction en sens inverse à chaque objets touchés
    }
}
