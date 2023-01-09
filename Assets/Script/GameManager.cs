using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    public static GameManager instance; //outros scriptes possam acessar variáveis ou metodos publicos desta classe;


    //variaveis arvore
    public GameObject[] troncos;
    public List<GameObject> listaTroncos;
    private float alturaTronco = 2.43f;
    private float posicaoInicialY = -2.38f;
    private int maxTroncos = 6;
    private bool troncosSemGalho = false;

    //variaveis de pontuacao
    public Text pontuacao;
    private int pontos = 0;

    //variaveis de tempo
    public Image barraTempo;

    private float larguraBarra = 188f;
    private float tempoJogo = 10f;
    private float tempoExtra = 0.115f;
    private float tempoAtual;



    public bool gameOver = false;

    void Awake()    {
        if (instance == null)        {
            instance = this;
        }

    }

    // Start is called before the first frame update
    void Start()    {

        tempoAtual = tempoJogo;

        InicializaTroncos();

    }

    // Update is called once per frame
    void Update()    {

        if (!gameOver) {

            DiminuirBarra();
                   

            
            }

    }
    void CriaTroncos(int posicao)
    {

        GameObject tronco = Instantiate(troncosSemGalho ? troncos[Random.Range(0, 3)] : troncos[0]);
        tronco.transform.localPosition = new Vector3(0f, posicaoInicialY + posicao * alturaTronco, 0f);
        listaTroncos.Add(tronco);
        troncosSemGalho = !troncosSemGalho;

    }

    void InicializaTroncos()    {
        for (int posicao = 0; posicao <= maxTroncos; posicao++)        {
            CriaTroncos(posicao);

        }



    }

    void CortaTroncos ()    {
        Destroy(listaTroncos[0]);
        listaTroncos.RemoveAt(0);
        SoundManager.instance.PlayFx(SoundManager.instance.fxCorte);
    }

    void ReposicionaTronco()    {
        for (int posicao = 0; posicao < listaTroncos.Count; posicao++)        {
            listaTroncos[posicao].transform.localPosition = new Vector3(0f, posicaoInicialY + posicao * alturaTronco, 0f);


        }

        CriaTroncos (maxTroncos);
    }

    void SomaPontos() {
        pontos++;
        pontuacao.text = pontos.ToString();
                         }

    void SomaTempo()
    {
        if (tempoAtual + tempoExtra < tempoJogo)        {
            tempoAtual = tempoAtual + tempoExtra;



        }


    }



    void DiminuirBarra ()    {
        tempoAtual -= Time.deltaTime;

        float tempo = tempoAtual / tempoJogo;
        float pos = larguraBarra - (tempo * larguraBarra);

        barraTempo.transform.localPosition = new Vector2(-pos, barraTempo.transform.localPosition.y);

        if (tempoAtual <= 0f)        {
            gameOver = true;
            SalvaPontuacao();
            
        }



    }

    public void SalvaPontuacao()    {

        if (PlayerPrefs.GetInt("best") < pontos) {
            PlayerPrefs.SetInt("best", pontos);
            }

        PlayerPrefs.SetInt("score", pontos);

        SoundManager.instance.PlayFx(SoundManager.instance.fxMorte);

        Invoke("ChamaCenaGameOver", 2f);

        
    }

    public void ChamaCenaGameOver() {


        if (PlayerPrefs.GetInt("Recorde") > pontos) {
            PlayerPrefs.SetInt("Recorde", pontos);
              }

        SceneManager.LoadScene("GameOver");
    }


    public void Toque()  {

        if (!gameOver) {

            CortaTroncos ();
            ReposicionaTronco ();
            SomaPontos();
            SomaTempo();
            


        }


    }



}
