using System;
using TechTalk.SpecFlow;

namespace backendtest.Steps
{
    [Binding]
    public class DesenvolvedorSteps
    {
        [Given(@"que um desenvolvedor será cadastrado")]
        public void DadoQueUmDesenvolvedorSeraCadastrado()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"todos os dados do desenvolvedor estão corretos")]
        public void QuandoTodosOsDadosDoDesenvolvedorEstaoCorretos()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"alguma informação do desenvolvedor está inválida")]
        public void QuandoAlgumaInformacaoDoDesenvolvedorEstaInvalida()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"retorna Ok com as informações do desenvolvedor")]
        public void EntaoRetornaOkComAsInformacoesDoDesenvolvedor()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"retorna BadRequest com validações do desenvolvedor")]
        public void EntaoRetornaBadRequestComValidacoesDoDesenvolvedor()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"que um desenvolvedor será excluído")]
        public void DadoQueUmDesenvolvedorSeraExcluido()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"retorna BadRequest com validações")]
        public void EntaoRetornaBadRequestComValidacoes()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"que o cadastro de um desenvolvedor será alterado")]
        public void DadoQueOCadastroDeUmDesenvolvedorSeraAlterado()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
