using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Seleniumtests_Podstavlennikova;

public class SeleniumTestsForPractic
{
    public ChromeDriver driver;

    [SetUp]
    public void SetUp()
    {
        var options = new ChromeOptions();
        options.AddArguments("--no-sandbox", "--disable-extensions");
        driver = new ChromeDriver(options);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        Autorization();
    }
    
    [Test]
    public void Authorization()
    {
        var currentUrl = driver.Url;
        Assert.That(currentUrl == "https://staff-testing.testkontur.ru/news",
            "current url = " + currentUrl + " а должен быть https://staff-testing.testkontur.ru/news");
    }

    [Test]
    public void NavigationTest()
    {
        sideMenuClick();
        
        //клик на Сообщества
        var community = driver.FindElements(By.CssSelector("[data-tid='Community']"))
            .First(element => element.Displayed);
        community.Click();
        
        // проверка урла и наличия заголовка Сообщества
        var communityTitle = driver.FindElement(By.CssSelector("[data-tid='Title']"));   
        Assert.That(communityTitle.Text == "Сообщества", "Заголовок должен быть Сообщества");
    }

    [Test]
    public void CreateEvent()
    {
        // Перейти на страницу мероприятия
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/events");
        
        // Клик на кнопку Создать
        var create = driver.FindElement(By.CssSelector("[data-tid='AddButton']"));
        create.Click(); 
        
        // Указать Название
        var eventTitle = driver.FindElement(By.CssSelector("[data-tid='Name']"));
        eventTitle.SendKeys("Этот ивент создал автотест");
        
        // Указать ИНН
        var inn = driver.FindElement(By.CssSelector("[placeholder='Введите ИНН']"));
        inn.SendKeys("574388660456");
        
        // Проставить признак Полный день 
        var alllDay = driver.FindElement(By.CssSelector("[data-tid='AllDay']"));
        alllDay.Click();
        
        // Клик на кнопку Создать
        var createButton = driver.FindElement(By.CssSelector("[data-tid='CreateButton']"));
        createButton.Click(); 
        
        // Проверяем, что попали на настройки мероприятия по наличию элемента Удалить мероприятие
        var settings = driver.FindElement(By.CssSelector("[data-tid='DeleteButton']"));   
        Assert.That(settings.Text == "Удалить мероприятие", "Не найден элемент Удалить мероприятие");
    }

    [Test]
    public void SignOut()
    {
        // Клик на аву
        var avatar = driver.FindElement(By.CssSelector("[data-tid='Avatar']"));   
        avatar.Click(); 
        
        //Выйти
        var logout = driver.FindElement(By.CssSelector("[data-tid='Logout']"));   
        logout.Click(); 
        
        // Проверить наличие Account/Logout в урле
        var logouturl = driver.Url;
        Assert.That(logouturl.Contains("Account/Logout"), "В урле должно содержаться Account/Logout");
    }

    [Test]
    public void ChangeLog()
    {
        sideMenuClick();
        // Клик на версию
        var version = driver.FindElements(By.CssSelector("[data-tid='Version']"))
            .Last(element => element.Displayed); 
        version.Click(); 
        
        // Явное ожидание открытия модалки
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
        wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[data-tid='modal-content']")));
        
        // Проверка открытия модального окна журнала
        var titleVersion = driver.FindElement(By.CssSelector("[data-tid='modal-content']"));
        Assert.That(titleVersion.Displayed, "Не открылось модальное окно журнала изменений");
        
    }
    
    public void Autorization()
    {
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
        
        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("marypodst@mail.ru");
        var password = driver.FindElement(By.Name("Password"));
        password.SendKeys("YaMahaATiNet2.");
          
        var enter = driver.FindElement(By.Name("button"));
        enter.Click(); 
        
        var news = driver.FindElement(By.CssSelector("[data-tid='Feed']"));
    }

    public void sideMenuClick()
    {
        var sideMenu = driver.FindElement(By.CssSelector("[data-tid='SidebarMenuButton']"));
        sideMenu.Click();

    }
    
      [TearDown]
       public void TearDown()
       { 
           //закрыть браузер и убить процесс драйвера
           driver.Quit();
       }
}
