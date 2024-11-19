using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Warren.Tools.Domain.Services;

namespace Warren.Tools.Application.Services
{
    public class RoboService : IRoboService
    {
        private string loginUrl = "http://jdspb62287735prod.pstianbima.rtm/JDCabineWeb/dll/JDCabineWEB.dll/login"; // Defina o URL de login
        private string jdUsername = "jaelson.lima"; // Defina o nome de usuário
        private string jdPassword = "Warren2024@"; // Defina a senha
        private string balanceUrl = "http://jdspb62287735prod.pstianbima.rtm/JDCabineWeb/dll/JDCabineWEB.dll/FINConcSTR?Menu=S"; // Defina a URL de saldo
        private string responseMsgUrl = "http://jdspb62287735prod.pstianbima.rtm/JDCabineWeb/dll/JDCabineWEB.dll/consultarmsg";
        
        public string IniciarRobo()
        {
            string saldo = "";
            Console.WriteLine("Initializing Chrome Driver: ");
            ChromeDriver driver = new ChromeDriver(GetHeadlessChromeOptions());
            Console.WriteLine("OK");

            try
            {
                // Fazer Login
                Login(driver);

                // Consultar saldo da conta
                ConsultarSaldo(driver);

                // Navegar para página de mensagens
                driver.Navigate().GoToUrl(responseMsgUrl);
                FiltrarMensagensSTR0013(driver);

                // Verificar se existe a mensagem "Respondida" no grid
                string numeroMsg = VerificarRespondida(driver);

                if (!string.IsNullOrEmpty(numeroMsg))
                {
                    // Criar a URL com o numeroMsg encontrado
                    //string urlResponseStr0013R1 = $"http://jdspb62287735prod.pstianbima.rtm/JDCabineWeb/dll/JDCabineWEB.dll/detalharmsg?NumMsg={numeroMsg}&DetalheResp=S";
                    string urlResponseStr0013R1 = $"http://jdspb62287735prod.pstianbima.rtm/JDCabineWeb/dll/JDCabineWEB.dll/detalharmsg?NumMsg={numeroMsg}&Voltar_DetalharMsg=/JDCabineWeb/dll/JDCabineWEB.dll/ConsultarMsg";

                    // Navegar para a URL
                    driver.Navigate().GoToUrl(urlResponseStr0013R1);
                    
                    IWebElement linkSTR0013R1 = driver.FindElement(By.LinkText("STR0013R1"));
                    linkSTR0013R1.Click();

                    // Obter saldo da página de resposta
                    saldo = ObterSaldo(driver);
                    Console.WriteLine("Saldo STR: " + saldo);
                }
                else
                {
                    Console.WriteLine("Mensagem 'Respondida' não encontrada após 3 tentativas.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                driver.Quit();
            }

            return saldo;
        }

        private void Login(ChromeDriver driver)
        {
            driver.Navigate().GoToUrl(loginUrl);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));

            IWebElement campoUsuario = driver.FindElement(By.Name("edtUsuario"));
            campoUsuario.SendKeys(jdUsername);

            IWebElement campoSenha = driver.FindElement(By.Name("edtSenha"));
            campoSenha.SendKeys(jdPassword);
            campoSenha.SendKeys(Keys.Tab);

            IWebElement botaoLogin = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("btnEntrar")));
            botaoLogin.Click();
        }

        private void ConsultarSaldo(ChromeDriver driver)
        {
            driver.Navigate().GoToUrl(balanceUrl);
            IWebElement btnConsultarSaldo = driver.FindElement(By.Name("btnConsSaldoSTR0013"));
            btnConsultarSaldo.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//td[contains(text(), 'Saldo STR:')]")));
        }

        private void FiltrarMensagensSTR0013(ChromeDriver driver)
        {
            IWebElement selectElement = driver.FindElement(By.Name("SelGrupoMsg"));
            SelectElement select = new SelectElement(selectElement);
            select.SelectByValue("STR");

            IWebElement selectElement2 = driver.FindElement(By.Name("SelCodMsg"));
            SelectElement select2 = new SelectElement(selectElement2);
            select2.SelectByValue("STR0013");

            IWebElement btnPesquisar = driver.FindElement(By.Name("btnPesquisar"));
            btnPesquisar.Click();
        }

        private string VerificarRespondida(ChromeDriver driver)
        {
            // Definir tempo de espera de 10 segundos
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            int maxRetries = 5;
            int retryCount = 0;
            bool isRespondidaFound = false;
            string numeroMsg = "";

            while (!isRespondidaFound && retryCount < maxRetries)
            {
                try
                {
                    // Encontrar a primeira linha do grid
                    IWebElement trElement = driver.FindElement(By.XPath("//tr[@class='Grid_Linha_1']"));
                    IList<IWebElement> tdElements = trElement.FindElements(By.TagName("td"));

                    // Pegar o número da mensagem (assumindo que está no primeiro td)
                    numeroMsg = tdElements[0].Text.Trim();

                    // Esperar até que o status "Respondida" seja encontrado
                    wait.Until(drv => tdElements.Any(td => td.Text.Trim() == "Respondida"));

                    // Se "Respondida" for encontrada, prosseguir
                    isRespondidaFound = true;
                    Console.WriteLine("Mensagem 'Respondida' encontrada.");
                }
                catch (WebDriverTimeoutException)
                {
                    retryCount++;
                    Console.WriteLine($"'Respondida' não encontrada. Tentando novamente em 10 segundos... (Tentativa {retryCount} de {maxRetries})");
                }
            }

            return numeroMsg; // Retorna o numeroMsg encontrado ou vazio
        }

        private string ObterSaldo(ChromeDriver driver)
        {
            //IWebElement saldoElement = driver.FindElement(By.XPath("//td[contains(text(), 'Saldo Reservas_Bancárias ou Conta_de_Liquidação:')]/following-sibling::td"));
            IWebElement saldoElement = driver.FindElement(By.XPath("//tr[td[text()='Saldo Reservas_Bancárias ou Conta_de_Liquidação']]"));
            
            IList<IWebElement> tdElements = saldoElement.FindElements(By.TagName("td"));
    
            // O saldo está na segunda célula <td> (índice 1)
            string saldo = tdElements[2].Text.Trim();
            
            return saldo;
        }

        private ChromeOptions GetHeadlessChromeOptions()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--headless");               // Executar sem interface gráfica
            options.AddArguments("--no-sandbox");             // Requerido para execução em containers
            options.AddArguments("--disable-dev-shm-usage");  // Superar problemas com recursos limitados
            options.AddArguments("--remote-allow-origins=*"); // Permitir conexões remotas
            options.AddArguments("--disable-gpu");            // Desabilitar GPU para ambientes headless
            options.AddArguments("--disable-software-rasterizer"); // Corrigir problemas de renderização com GPU
            options.AddArguments("--disable-extensions");     // Desabilitar extensões
            options.AddArguments("--disable-infobars");       // Desabilitar barras de informação
            options.AddArguments("--disable-browser-side-navigation"); // Prevenir problemas de navegação
            return options;
        }
    }
}
