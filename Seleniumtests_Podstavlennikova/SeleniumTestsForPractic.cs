using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Seleniumtests_Podstavlennikova;

public class SeleniumTestsForPractic
{
    [Test]
    public void Authorization()
    {
        var options = new ChromeOptions();
        options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");
        
        //зайти в хром с помощью веб драйвера
        var driver = new ChromeDriver();
        
        //перейти по урлу https://staff-testing.testkontur.ru/ 
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
        Thread.Sleep(3000);
        
        //ввести логин и пароль
        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("marypodst@mail.ru");

        var password = driver.FindElement(By.Name("Password"));
        password.SendKeys("YaMahaATiNet2.");
     
        
        //нажать кнопку Войти       
        var enter = driver.FindElement(By.Name("button"));
        enter.Click();

        
        //проверить, что находимся на нужной странице
        var currentUrl = driver.Url;
        Assert.That(currentUrl == "https://staff-testing.testkontur.ru/news");
        
      //добавила коммент для тестового коммита
       
        
        //закрыть браузер и убить процесс драйвера
        driver.Quit();
    }
    
    
}
