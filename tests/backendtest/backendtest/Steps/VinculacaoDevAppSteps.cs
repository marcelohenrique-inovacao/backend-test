using System;
using TechTalk.SpecFlow;

namespace backendtest.Steps
{
    [Binding]
    public class VinculacaoDevAppSteps
    {
        [Given(@"que um desenvolvedor será desvinculado de um aplicativo")]
        public void DadoQueUmDesenvolvedorSeraDesvinculadoDeUmAplicativo()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"todos os dados do desvínculo estão corretos")]
        public void QuandoTodosOsDadosDoDesvinculoEstaoCorretos()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"alguma informação do desvínculo está inválida")]
        public void QuandoAlgumaInformacaoDoDesvinculoEstaInvalida()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"retorna Ok com as informações do desvínculo")]
        public void EntaoRetornaOkComAsInformacoesDoDesvinculo()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"retorna BadRequest com validações do desvínculo")]
        public void EntaoRetornaBadRequestComValidacoesDoDesvinculo()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"que um desenvolvedor será vinculado à um aplicativo")]
        public void DadoQueUmDesenvolvedorSeraVinculadoAUmAplicativo()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"todos os dados do vínculo estão corretos")]
        public void QuandoTodosOsDadosDoVinculoEstaoCorretos()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"retorna Ok com as informações do vínculo")]
        public void EntaoRetornaOkComAsInformacoesDoVinculo()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"alguma informação do vínculo está inválida")]
        public void QuandoAlgumaInformacaoDoVinculoEstaInvalida()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"retorna BadRequest com validações do vínculo")]
        public void EntaoRetornaBadRequestComValidacoesDoVinculo()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
