import { browser, by, element } from 'protractor';
import { protractor } from 'protractor/built/ptor';

export class AppPage {
    navigateToHomePage() {
        return browser.get('/');
    }

    navigateToLogin() {
        return browser.get('/login');
    }

    navigateToInvitePage() {
        return browser.get('/invite');
    }

    navigateToAllRecordsPage() {
        return browser.get('/trips');
    }


    login() {
        browser.get('/login');
        element(by.css('input[type="email"')).clear();
        element(by.css('input[type="email"')).sendKeys('admin@travelplanner');
        element(by.css('input[type="password"')).clear();
        element(by.css('input[type="password"')).sendKeys('password');
        element(by.css('input[type="submit"')).click();
        browser.wait(x => browser.getCurrentUrl().then(x => x.indexOf('login') < 0), 5000);
    }

    navigateToRegister() {
        return browser.get('/register');
    }

    location() {
        return browser.getCurrentUrl();
    }

    getParagraphText() {
        return element(by.css('app-root h1')).getText();
    }

    fillValidRegistrationInfo() {
        element(by.css('input[type="email"')).clear();
        element(by.css('input[type="email"')).sendKeys('dantee91@yahoo.com');
        element(by.id('inputPassword')).clear();
        element(by.id('inputPassword')).sendKeys('123456');
        element(by.id('inputConfirmPassword')).clear();
        element(by.id('inputConfirmPassword')).sendKeys('123456');
        element(by.css('input[type="submit"')).click();
    }

    clearStorage() {
        browser.executeScript('window.localStorage.clear()');
    }

    numberOfTrips() {
        return element.all(by.tagName('tr')).count()
    }

    waitTillThereAreTrips(milliseconds = 3000) {
        browser.wait(() => this.numberOfTrips().then(x => x > 0), milliseconds);
    }
    waitOneSecond() {
        let d = new Date().getTime();
        browser.wait(() => (new Date().getTime() - d) > 1000, 5000);
    }

}
