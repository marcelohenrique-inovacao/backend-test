using System;
using TechTalk.SpecFlow;

namespace backendtest.Steps
{
    [Binding]
    public class AplicativoSteps
    {
        [Given(@"que um aplicativo será cadastrado")]
        public void DadoQueUmAplicativoSeraCadastrado()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"todos os dados do aplicativo estão corretos")]
        public void QuandoTodosOsDadosDoAplicativoEstaoCorretos()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"alguma informação do aplicativo está inválida")]
        public void QuandoAlgumaInformacaoDoAplicativoEstaInvalida()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"retorna Ok com as informações do aplicativo")]
        public void EntaoRetornaOkComAsInformacoesDoAplicativo()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"retorna BadRequest com validações do aplicativo")]
        public void EntaoRetornaBadRequestComValidacoesDoAplicativo()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"que o cadastro de um aplicativo será alterado")]
        public void DadoQueOCadastroDeUmAplicativoSeraAlterado()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"todos os dados estão do aplicativo corretos")]
        public void QuandoTodosOsDadosEstaoDoAplicativoCorretos()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"que um aplicativo será excluído")]
        public void DadoQueUmAplicativoSeraExcluido()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"não está vinculado à um desenvolvedor")]
        public void QuandoNaoEstaVinculadoAUmDesenvolvedor()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"estiver vinculado à um desenvolvedor")]
        public void QuandoEstiverVinculadoAUmDesenvolvedor()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
