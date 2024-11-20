### Do que se trata?
R: aplicação para checar alguma notícia ou informação através de portais de noticias.

## API

Backend baseado em REST API.

A sua principal rota:
```curl
  POST /
```
 Requer um corpo de requesito que contenha a manchete ou palavras-chave.
 
Body:
```
{
    'keywords': 'whatsapp indenização'
}
```

Resposta:

```JSON
  {
    "keywords": "whatsapp indenização",
    "checkingSources": [
        {
            "veridict": "enganoso",
            "sourceSite": "Uol Confere",
            "link": "https://noticias.uol.com.br/confere/ultimas-noticias/2024/10/17/indenizacao-vazamento-dados-whatsapp-golpe.htm",
            "relatedHeadlines": [
                "Post tira de contexto fala de Érika Hilton sobre PEC contra escala 6x1",
                "Ida de Bolsonaro à posse de Trump depende do STF",
                "Homem que faz gestos ofensivos à torcida do Atlético-MG não é José Dirceu",
                "Aeronaves estrangeiras mostradas em vídeo participavam de simulação da FAB",
                "Trump não disse que negará negócios com Brasil; legenda de vídeo é falsa",
                "É falso que cantora ateou fogo na Bíblia em festival do MST",
                "Negacionismo climático na COP29 tem preço",
                "Post sobre limusine da Tesla para Trump foi feito por perfil de paródias",
                "É falso que Maduro pediu ajuda ao papa após vitória de Trump em 2024",
                "PEC de Erika Hilton pelo fim da escala de trabalho 6x1 ainda não foi votada",
                "Trump cogitou ação militar na Venezuela em 2017; declaração não é recente"
            ]
        },
        {
            "veridict": "enganoso",
            "sourceSite": "Estadão Verifica",
            "link": "https://www.estadao.com.br/estadao-verifica/whatsapp-30-mil-indenizacao-programa-encontro-falso/",
            "relatedHeadlines": [
                "Ida de Bolsonaro à posse de Trump depende do STF,  não de passaportes da Itália e dos EUA",
                "É falso que autor de atentado em Brasília tenha sido morto a tiros por seguranças",
                "É falso post que promete cadastro no Auxílio Gas para beneficiários do Bolsa Família",
                "Vacina DTP não foi banida dos EUA nem causa danos cerebrais ou mortes",
                "Aeronaves estrangeiras mostradas em vídeo participavam de simulação da FAB; entenda"
            ]
        }
    ]
}
```
