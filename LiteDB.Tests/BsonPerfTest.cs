﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LiteDB;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnitTest
{
    [TestClass]
    public class BsonPerfTest
    {
        [TestMethod]
        public void BsonWritePerf_Test()
        {
            // Usando a versão inicial, pra 20k
            // time to convert  - 1979732
            // Time to copy     -   77853

            var json = "{\"suggestions\":[{\"id\":1008,\"name\":\"Acústico Cover\",\"description\":\"Versões fofinhas de grandes sucessos do pop e rock internacionais.\",\"key\":\"acustico-cover\",\"arts\":{\"2x2\":\"http://lists00.cdn.superplayer.fm/0/e/0e0fb5d9-66fa-43fb-a363-e189f0d9464e.jpg\",\"2x1\":\"http://lists00.cdn.superplayer.fm/c/7/c7db7c2f-49e3-404c-bb5f-d76fe4be09e2.jpg\"},\"color\":\"#2cb2bf\",\"suggestion\":{\"reason\":\"lastplayed\"},\"favorite\":false,\"updated\":true},{\"id\":296,\"name\":\"Arrumando as Malas\",\"description\":\"Atenção passageiros do voo Superplayer 042, embarque imediato pela lista de número 296 com destino à felicidade.\",\"key\":\"para-arrumar-malas\",\"arts\":{\"2x2\":\"http://lists01.cdn.superplayer.fm/1/6/16e70ddd-8e8c-4acf-b8eb-a63d7ddb9a17.jpg\",\"2x1\":\"http://lists00.cdn.superplayer.fm/c/d/cd99fc5a-a6dc-451c-a7d8-0c84a7c061e2.jpg\"},\"color\":\"#60c2c4\",\"suggestion\":{\"reason\":\"lastplayed\"},\"favorite\":false,\"updated\":false},{\"id\":840,\"name\":\"Manhãs Calminhas\",\"description\":\"Você pode estar de férias, desempregado, ser seu próprio chefe ou não ligar pra pontualidade. Dê o play aqui e aproveite essa manhã!\",\"key\":\"para-manhas-tranquilas-calma\",\"arts\":{\"2x2\":\"http://lists01.cdn.superplayer.fm/1/9/19fbac76-26a0-4538-aeab-f813b6b99622.jpg\",\"2x1\":\"http://lists00.cdn.superplayer.fm/0/2/024b0f12-7c5f-4df0-9123-a799f439b415.jpg\"},\"color\":\"#828bc3\",\"suggestion\":{\"reason\":\"mostplayed\"},\"favorite\":false,\"updated\":false},{\"id\":177,\"name\":\"Rock Gaúcho\",\"description\":\"Pega a chinoca, monta no cavalo\r\ne desbrava essa lista cheia de clássicos do rock gauchesco, tchê!\",\"key\":\"rock-gaucho\",\"color\":\"#2cb2bf\",\"suggestion\":{\"reason\":\"mostplayed\"},\"favorite\":false,\"updated\":false},{\"id\":670,\"name\":\"Acampando\",\"description\":\"Que tipo de pássaro você é? Se você for do tipo que gosta de acampar fora daquele som comercial, dê uma bicada aqui.\",\"key\":\"para-acampar\",\"arts\":{\"2x2\":\"http://lists01.cdn.superplayer.fm/7/d/7d4e68cf-cd0f-4969-94ac-54cf4981794e.jpg\",\"2x1\":\"http://lists00.cdn.superplayer.fm/c/1/c1513f1d-8af8-4c7e-9b62-beeaf0bee222.jpg\"},\"color\":\"#ce1560\",\"suggestion\":{\"reason\":\"relatedlastplayed\",\"playlist\":{\"id\":296,\"name\":\"Arrumando as Malas\",\"key\":\"para-arrumar-malas\",\"favorite\":false,\"updated\":false}},\"favorite\":false,\"updated\":false},{\"id\":201,\"name\":\"Lareira e chocolate quente\",\"description\":\"Músicas aconchegantes para viajar olhando pro fogo da lareira. Só cuidado pra não queimar a língua no chocolate quente.\",\"key\":\"inverno\",\"arts\":{\"2x2\":\"http://lists00.cdn.superplayer.fm/9/0/90eed4e8-c9e8-43aa-b0d8-aa143e9ddaa4.jpg\",\"2x1\":\"http://lists00.cdn.superplayer.fm/6/7/67f9f311-32c4-442f-adb9-d6f4609419e8.jpg\"},\"color\":\"#48cc8a\",\"suggestion\":{\"reason\":\"relatedlastplayed\",\"playlist\":{\"id\":840,\"name\":\"Manhãs Calminhas\",\"key\":\"para-manhas-tranquilas-calma\",\"favorite\":false,\"updated\":false}},\"favorite\":false,\"updated\":false},{\"id\":860,\"name\":\"Sonhando Acordado\",\"description\":\"Para você que tá meio distante. Tem alguém aí?\",\"key\":\"sonhando-acordado\",\"arts\":{\"2x2\":\"http://lists01.cdn.superplayer.fm/a/e/aece78a9-26da-4ad9-b678-89e44078939c.jpg\",\"2x1\":\"http://lists02.cdn.superplayer.fm/b/c/bcf262b8-b928-4c7e-8e65-31390cddcfa1.jpg\"},\"color\":\"#48cc8a\",\"suggestion\":{\"reason\":\"relatedlastplayed\",\"playlist\":{\"id\":296,\"name\":\"Arrumando as Malas\",\"key\":\"para-arrumar-malas\",\"favorite\":false,\"updated\":false}},\"favorite\":false,\"updated\":false},{\"id\":731,\"name\":\"De Conchinha\",\"description\":\"A trilha ideal para aquele momento amorzinho de ficar perto, juntinho, com cabelo na cara e braço dormente. Mas é bom. <3\",\"key\":\"de-conchinha\",\"arts\":{\"2x2\":\"http://lists01.cdn.superplayer.fm/1/7/1792d012-c5a3-40ea-b9c4-750c990ffdf8.jpg\",\"2x1\":\"http://lists02.cdn.superplayer.fm/2/a/2ac0a72e-bc10-4241-89b2-dc8205071a15.jpg\"},\"color\":\"#662d91\",\"suggestion\":{\"reason\":\"relatedlastplayed\",\"playlist\":{\"id\":840,\"name\":\"Manhãs Calminhas\",\"key\":\"para-manhas-tranquilas-calma\",\"favorite\":false,\"updated\":false}},\"favorite\":false,\"updated\":false},{\"id\":232,\"name\":\"Pedalando \",\"description\":\"Pra trocar o carro pela bike e aproveitar a vida! =-)\",\"key\":\"bicicleta\",\"arts\":{\"2x2\":\"http://lists00.cdn.superplayer.fm/6/1/615b5c82-2aeb-42cb-b8fd-9e6f6f25fcc7.jpg\",\"2x1\":\"http://lists00.cdn.superplayer.fm/6/e/6efc656b-e6e8-4b5c-b840-b557d1887e63.jpg\"},\"color\":\"#2cb2bf\",\"suggestion\":{\"reason\":\"relatedlastplayed\",\"playlist\":{\"id\":840,\"name\":\"Manhãs Calminhas\",\"key\":\"para-manhas-tranquilas-calma\",\"favorite\":false,\"updated\":false}},\"favorite\":false,\"updated\":false},{\"id\":15,\"name\":\"Indie Folk\",\"description\":\"Fogueiras, cabanas no meio do mato e essa playlist pra armar o cenário ideal. \",\"key\":\"indie-folk\",\"color\":\"#60c2c4\",\"suggestion\":{\"reason\":\"relatedlastplayed\",\"playlist\":{\"id\":840,\"name\":\"Manhãs Calminhas\",\"key\":\"para-manhas-tranquilas-calma\",\"favorite\":false,\"updated\":false}},\"favorite\":false,\"updated\":false},{\"id\":82,\"name\":\"Pegando no Sono\",\"description\":\"Notas perfeitamente combinadas pra você não precisar contar ovelhas.\",\"key\":\"para-dormir\",\"arts\":{\"2x2\":\"http://lists00.cdn.superplayer.fm/9/e/9eb3d46f-7c40-44c4-9a25-f835d6a54958.jpg\",\"2x1\":\"http://lists01.cdn.superplayer.fm/4/8/48a41b36-e5de-4191-b685-7b220700c4bf.jpg\"},\"color\":\"#f7931e\",\"suggestion\":{\"reason\":\"relatedlastplayed\",\"playlist\":{\"id\":296,\"name\":\"Arrumando as Malas\",\"key\":\"para-arrumar-malas\",\"favorite\":false,\"updated\":false}},\"favorite\":false,\"updated\":true},{\"id\":357,\"name\":\"Coolzinhando\",\"description\":\"A melhor trilha para colocar em prática as receitas do Jamie Oliver. Sem pressão, só diversão.\",\"key\":\"coolzinhando\",\"arts\":{\"2x2\":\"http://lists02.cdn.superplayer.fm/8/5/85e60977-d236-4522-ac31-ffb585ebb7af.jpg\",\"2x1\":\"http://lists01.cdn.superplayer.fm/d/0/d0c477cd-48f6-4329-8ac4-9db2d5e59c92.jpg\"},\"color\":\"#2cb2bf\",\"suggestion\":{\"reason\":\"relatedlastplayed\",\"playlist\":{\"id\":296,\"name\":\"Arrumando as Malas\",\"key\":\"para-arrumar-malas\",\"favorite\":false,\"updated\":false}},\"favorite\":false,\"updated\":false},{\"id\":355,\"name\":\"Café da Manhã\",\"description\":\"A trilha que deixará seu café da manhã tão perfeito quanto o de um comercial de margarina.\",\"key\":\"cafe-da-manha\",\"arts\":{\"2x2\":\"http://lists02.cdn.superplayer.fm/b/3/b34b1dbe-de8d-40c3-a044-6c9c9152e0ca.jpg\",\"2x1\":\"http://lists02.cdn.superplayer.fm/2/2/2221fc45-8c08-49f9-a57a-c13f2bd3e7bc.jpg\"},\"color\":\"#e5176b\",\"sponsor\":{\"name\":\"CanecaTag\",\"link\":\"http://bit.ly/sp-canecatag-versite\",\"logo\":\"http://ads00.cdn.superplayer.fm/9/f/9ff99244-7ebe-4bf6-8c35-b1ac547eb97e.png\",\"logoReverse\":\"http://ads02.cdn.superplayer.fm/5/c/5cc12450-454c-41e1-ba42-c9ce4ac4c5a1.png\"},\"suggestion\":{\"reason\":\"relatedlastplayed\",\"playlist\":{\"id\":296,\"name\":\"Arrumando as Malas\",\"key\":\"para-arrumar-malas\",\"favorite\":false,\"updated\":false}},\"favorite\":false,\"updated\":false},{\"id\":361,\"name\":\"PicNic & Cupcakes\",\"description\":\"Músicas pra você curtir aquele solzinho do fim da tarde descalço na grama.\",\"key\":\"para-picnic\",\"color\":\"#f2a33b\",\"suggestion\":{\"reason\":\"relatedlastplayed\",\"playlist\":{\"id\":840,\"name\":\"Manhãs Calminhas\",\"key\":\"para-manhas-tranquilas-calma\",\"favorite\":false,\"updated\":false}},\"favorite\":false,\"updated\":false}],\"highlights\":[{\"id\":988,\"name\":\"Olla - The Balada Never Ends\",\"description\":\"Várias baladas, uma dica: com Olla, #TheBaladaNeverEnds. \",\"key\":\"olla-the-balada-never-ends\",\"arts\":{\"2x2\":\"http://lists02.cdn.superplayer.fm/2/1/21ff4200-2f94-450b-9495-4db489954ea5.jpg\",\"2x1\":\"http://lists01.cdn.superplayer.fm/a/9/a9588b81-49e7-4bb0-9ce9-b0e3d7292878.jpg\"},\"color\":\"#0d0e00\",\"sponsor\":{\"name\":\"Olla\",\"link\":\"http://bit.ly/sp-olla-versite-outubro\",\"logo\":\"http://ads00.cdn.superplayer.fm/c/6/c610fc8d-5fc4-4a16-9883-3c53102a9709.png\"},\"custom\":true,\"paid\":true,\"favorite\":false,\"updated\":false},{\"id\":744,\"name\":\"50 Tons de Blues\",\"description\":\"Pra quem prefere um blues ao cinza, e sente prazer é pelas guitarras de B. B. King.\",\"key\":\"50-tons-blues\",\"arts\":{\"2x2\":\"http://lists00.cdn.superplayer.fm/0/d/0d7ff11e-3e67-4797-aed4-9e1839f3ec4f.jpg\",\"2x1\":\"http://lists01.cdn.superplayer.fm/a/6/a686d2f4-0cb1-4949-8ca2-ec9e8eaf8f42.jpg\"},\"color\":\"#2cb2bf\",\"custom\":true,\"paid\":false,\"favorite\":false,\"updated\":false},{\"id\":927,\"name\":\"Fibz - Fibra Pro Seu Dia a Dia \",\"description\":\"Tem uma forma muito mais legal de garantir a força pra encarar o dia. Abrindo Fibz e dando play aqui!\",\"key\":\"fibz-fibra-pro-seu-dia-a-dia\",\"arts\":{\"2x2\":\"http://lists00.cdn.superplayer.fm/6/e/6e1cb14e-1316-47f6-a773-2545fe28879f.jpg\",\"2x1\":\"http://lists02.cdn.superplayer.fm/b/f/bf68c15e-8344-4c1c-9e12-a62b96ec4b69.jpg\"},\"color\":\"#e39600\",\"link\":\"http://bit.ly/sp-fibz-versite\",\"sponsor\":{\"name\":\"Fibz - Fibra Pro Seu Dia a Dia \",\"link\":\"http://bit.ly/sp-fibz-versite\",\"logo\":\"http://ads00.cdn.superplayer.fm/c/6/c6cebf41-5743-459d-a26b-a2e2e9d42380.png\"},\"custom\":true,\"paid\":true,\"favorite\":false,\"updated\":false},{\"id\":1007,\"name\":\"Case 2015\",\"description\":\"Hard work beats talent. Assinantes do Superplayer tem 40% de desconto na maior feira de empreendedorismo da América Latina.\",\"key\":\"case-2015\",\"arts\":{\"2x2\":\"http://lists01.cdn.superplayer.fm/d/c/dc244c66-59b9-4e86-801e-88e26b70e19f.jpg\",\"2x1\":\"http://lists00.cdn.superplayer.fm/9/0/900ebfee-fb3a-439c-a70c-be83c3f81d75.jpg\"},\"color\":\"#60c2c4\",\"link\":\"http://bit.ly/1O55ogq\",\"sponsor\":{\"name\":\"Case 2015\",\"link\":\"http://bit.ly/1O55ogq\",\"logo\":\"http://ads00.cdn.superplayer.fm/6/3/63cf6b21-8ed9-41e2-b1ca-3b57a26dfc5a.png\"},\"custom\":true,\"paid\":false,\"favorite\":false,\"updated\":false},{\"id\":979,\"name\":\"Unisinos - Escola Politécnica\",\"description\":\"A playlist com a cara da nossa escola: variada e cheia de qualidade.\",\"key\":\"unisinos-escola-politecnica\",\"arts\":{\"2x2\":\"http://lists00.cdn.superplayer.fm/9/0/908242d2-8696-4112-a579-5d96c71ab3be.jpg\",\"2x1\":\"http://lists00.cdn.superplayer.fm/f/6/f65f63b1-0b8b-429a-ba72-04809ca57475.jpg\"},\"color\":\"#2fbbb1\",\"sponsor\":{\"name\":\"Unisinos - Escola Politécnica\",\"link\":\"http://bit.ly/spl-unisinos-politecnica\",\"logo\":\"http://ads01.cdn.superplayer.fm/d/8/d86e8544-15a8-494c-8a0d-b83e2d8d91ce.png\"},\"custom\":true,\"paid\":true,\"favorite\":false,\"updated\":false},{\"id\":1004,\"name\":\"Escorpião\",\"description\":\"Uma playlist intensa, pra ser ouvida com moderação (se você for escorpiano, favor ignorar esse conselho).\",\"key\":\"signo-escorpiao\",\"arts\":{\"2x2\":\"http://lists02.cdn.superplayer.fm/5/1/5150bf52-a851-4f1c-8db0-de72309313ba.jpg\",\"2x1\":\"http://lists01.cdn.superplayer.fm/7/0/703e2d52-56f5-437f-9883-2b87ef6b17fc.jpg\"},\"color\":\"#48cc8a\",\"paid\":false,\"favorite\":false,\"updated\":true},{\"id\":999,\"name\":\"MBB - Música Bonitinha Brasileira\",\"description\":\"As musiquinhas nacionais mais fofas que você pode imaginar.\",\"key\":\"mbb\",\"arts\":{\"2x2\":\"http://lists02.cdn.superplayer.fm/2/7/274a422c-00e3-49a8-8bbc-c4d027b7d49b.jpg\",\"2x1\":\"http://lists02.cdn.superplayer.fm/b/6/b6e1216a-3454-4d69-ba37-ad2f8acfe5c4.jpg\"},\"color\":\"#48cc8a\",\"paid\":false,\"favorite\":false,\"updated\":false},{\"id\":1008,\"name\":\"Acústico Cover\",\"description\":\"Versões fofinhas de grandes sucessos do pop e rock internacionais.\",\"key\":\"acustico-cover\",\"arts\":{\"2x2\":\"http://lists00.cdn.superplayer.fm/0/e/0e0fb5d9-66fa-43fb-a363-e189f0d9464e.jpg\",\"2x1\":\"http://lists00.cdn.superplayer.fm/c/7/c7db7c2f-49e3-404c-bb5f-d76fe4be09e2.jpg\"},\"color\":\"#2cb2bf\",\"paid\":false,\"favorite\":false,\"updated\":true},{\"id\":129,\"name\":\"Nova Geração da MPB \",\"description\":\"Os novos nomes da MPB que vêm arrasando! Herdeiros ilegítimos e musicais de Caetano e Chico.\",\"key\":\"nova-geracao-da-mpb\",\"color\":\"#f7931e\",\"paid\":false,\"favorite\":false,\"updated\":false},{\"id\":210,\"name\":\"Livros e Jazz\",\"description\":\"Dá play e vá ler um livro. \",\"key\":\"para-ler-com-jazz\",\"arts\":{\"2x2\":\"http://lists00.cdn.superplayer.fm/f/b/fb4d5bfe-a006-4224-836e-b3cd091b69ec.jpg\",\"2x1\":\"http://lists02.cdn.superplayer.fm/b/d/bd17163d-82d6-4b23-9d55-83d85d93f83a.jpg\"},\"color\":\"#828bc3\",\"paid\":false,\"favorite\":false,\"updated\":false},{\"id\":756,\"name\":\"Top 50 Love\",\"description\":\"As 50 músicas mais amadas pelo público no último mês..\",\"key\":\"top-50-love\",\"arts\":{\"2x2\":\"http://lists02.cdn.superplayer.fm/e/f/ef5fb493-86ca-44f7-a7bf-7fca81a5f94a.jpg\",\"2x1\":\"http://lists00.cdn.superplayer.fm/f/b/fb621e0d-3b5c-4733-909b-6e65984f874a.jpg\"},\"color\":\"#2cb2bf\",\"custom\":true,\"paid\":false,\"favorite\":false,\"updated\":false},{\"id\":605,\"name\":\"Oktoberfest\",\"description\":\"Wir haben gehoert, dass, wenn Sie Deutsch nicht sprechen koennen, Sie einige  Pints Weissbier trinken koennten um die Worte fliessend zu machen.\",\"key\":\"oktoberfest\",\"arts\":{\"2x2\":\"http://lists00.cdn.superplayer.fm/c/d/cd2b4084-b4e8-435a-ab65-f28ac026d1b8.jpg\",\"2x1\":\"http://lists01.cdn.superplayer.fm/d/2/d21cbbc2-94b7-454d-b830-0c4ccaef31a7.jpg\"},\"color\":\"#ce1560\",\"custom\":true,\"paid\":false,\"favorite\":false,\"updated\":false}],\"alfred\":{\"playlists\":[{\"id\":855,\"name\":\"225 Músicas para a Viagem a Marte\",\"description\":\"Há vida em Marte? Posicione-se na cadeira e dê o sinal para o major Tom iniciar a decolagem. E boa viagem.\",\"key\":\"viagem-a-marte\",\"arts\":{\"2x2\":\"http://lists00.cdn.superplayer.fm/0/e/0e396660-0a60-43dd-9375-3feafe6180d7.jpg\",\"2x1\":\"http://lists00.cdn.superplayer.fm/0/a/0a91d575-f680-4845-8675-e914af124463.jpg\"},\"color\":\"#2cb2bf\",\"favorite\":false,\"updated\":false},{\"id\":961,\"name\":\"Vaporwave\",\"description\":\"Aqui a onda é: diferente, trash, retrô e experimental.\",\"key\":\"vaporwave\",\"arts\":{\"2x2\":\"http://lists01.cdn.superplayer.fm/7/d/7d510dce-f524-48a3-bfbe-de1ac7d1b450.jpg\",\"2x1\":\"http://lists00.cdn.superplayer.fm/c/f/cf815082-24d2-4de9-a0d6-b6d8166bc6bc.jpg\"},\"color\":\"#48cc8a\",\"favorite\":false,\"updated\":false},{\"id\":203,\"name\":\"Estimulando a Criatividade\",\"description\":\"Pra dar um gás e te deixar pronto pro toró de palpite!\",\"key\":\"para-criatividade\",\"arts\":{\"2x2\":\"http://lists01.cdn.superplayer.fm/7/6/76dd131b-affc-41ca-810d-598cb171d057.jpg\",\"2x1\":\"http://lists01.cdn.superplayer.fm/1/8/1818f82d-5c35-49f5-839e-ffe4cb18e864.jpg\"},\"color\":\"#f2a33b\",\"favorite\":false,\"updated\":true},{\"id\":783,\"name\":\"Brainstorming\",\"description\":\"Peraí, e se a gente fizesse uma playlist MAS que a pessoa conseguisse ouvir COM OS OLHOS?\",\"key\":\"brainstorming\",\"arts\":{\"2x2\":\"http://lists01.cdn.superplayer.fm/4/1/418f4e59-102f-49e3-b91b-1282afc48e5d.jpg\",\"2x1\":\"http://lists00.cdn.superplayer.fm/3/5/35d5cc1d-035a-495b-9751-bb3c8eed6ba1.jpg\"},\"color\":\"#48cc8a\",\"favorite\":false,\"updated\":false},{\"id\":21,\"name\":\"Indie Rock\",\"description\":\"Pra navegar no submundo meio inacessível do rock independente. \",\"key\":\"indie-rock\",\"color\":\"#ce1560\",\"favorite\":false,\"updated\":false},{\"id\":312,\"name\":\"Focando no Trabalho\",\"description\":\"Melhor que Ritalina!\",\"key\":\"super-concentrado\",\"arts\":{\"2x2\":\"http://lists01.cdn.superplayer.fm/a/3/a349b1bb-eb7b-4027-b80f-40b2ed3faa3c.jpg\",\"2x1\":\"http://lists00.cdn.superplayer.fm/3/0/30801d94-e0cc-4eb8-97aa-1171dfe8e370.jpg\"},\"color\":\"#2cb2bf\",\"favorite\":false,\"updated\":false},{\"id\":935,\"name\":\"Rock no Escritório\",\"description\":\"Pra bater o pé e usar o material do escritório como bateria improvisada.\",\"key\":\"rock-no-escritorio\",\"arts\":{\"2x2\":\"http://lists02.cdn.superplayer.fm/2/5/2555a9c5-e197-4225-bf78-a8941361e75e.jpg\",\"2x1\":\"http://lists02.cdn.superplayer.fm/e/4/e4391e4b-99be-4532-8415-8414cb22da8c.jpg\"},\"color\":\"#48cc8a\",\"favorite\":false,\"updated\":false},{\"id\":210,\"name\":\"Livros e Jazz\",\"description\":\"Dá play e vá ler um livro. \",\"key\":\"para-ler-com-jazz\",\"arts\":{\"2x2\":\"http://lists00.cdn.superplayer.fm/f/b/fb4d5bfe-a006-4224-836e-b3cd091b69ec.jpg\",\"2x1\":\"http://lists02.cdn.superplayer.fm/b/d/bd17163d-82d6-4b23-9d55-83d85d93f83a.jpg\"},\"color\":\"#828bc3\",\"favorite\":false,\"updated\":false}]}}";
            var o = JsonSerializer.Deserialize(json).AsDocument;

            var w1 = new Stopwatch();
            var w2 = new Stopwatch();

            for(var i = 0; i < 20000; i++)
            {
                w1.Start();
                var bson1 = BsonSerializer.Serialize(o);
                w1.Stop();

                w2.Start();
                var bson2 = BsonSerializer2.Serialize(o);
                w2.Stop();

            }

            Debug.WriteLine("Time to convert 1  " + w1.ElapsedTicks.ToString().PadLeft(15, ' '));
            Debug.WriteLine("Time to convert 2  " + w2.ElapsedTicks.ToString().PadLeft(15, ' '));

        }
    }
}
