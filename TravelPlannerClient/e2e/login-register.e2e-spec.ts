import { AppPage } from './app.po';
import { element, by, browser } from 'protractor';
import { protractor } from 'protractor/built/ptor';

describe('Login page', () => {
    let page: AppPage;

    beforeEach(() => {
        page = new AppPage();
    });

    beforeAll(() => {

    });

    afterEach(() => {
        page.clearStorage();
    });

    it('should redirect to login page', () => {
        page.navigateToHomePage();
        expect(page.location()).toContain("login");
    });

    it('should allow login', () => {
        page.navigateToLogin();
        element(by.css('input[type="email"')).clear();
        element(by.css('input[type="email"')).sendKeys('admin@travelplanner');
        element(by.css('input[type="password"')).clear();
        element(by.css('input[type="password"')).sendKeys('password');
        element(by.css('input[type="submit"')).click();

        browser.wait(() => page.location().then(url => url.indexOf('login') == -1), 1000);
        
        expect(page.location()).not.toContain('login');
    });

    it('shouldn\'t allow invalid login', () => {
        page.navigateToLogin();
        element(by.css('input[type="email"')).clear();
        element(by.css('input[type="email"')).sendKeys('muhammad@gmail.com');
        element(by.css('input[type="password"')).clear();
        element(by.css('input[type="password"')).sendKeys('123456');
        element(by.css('input[type="submit"')).click();

        expect(page.location()).toContain('login');
    });

    it('should request valid email before login', () => {
        page.navigateToLogin();
        element(by.css('input[type="email"')).clear();
        element(by.css('input[type="email"')).sendKeys('muhammadgmail.com');
        element(by.css('input[type="password"')).clear();
        element(by.css('input[type="password"')).sendKeys('123456');
        // element(by.css('input[type="submit"')).click();

        expect(element(by.css('.alert-danger')).getText()).toEqual('Email is unvalid');
        expect(page.location()).toContain('login');
    });

    it('shouldn\'t allow empty passwords to login', () => {
        page.navigateToLogin();
        element(by.css('input[type="email"')).clear();
        element(by.css('input[type="email"')).sendKeys('muhammad@gmail.com');
        element(by.css('input[type="password"')).clear();
        element(by.css('input[type="password"')).sendKeys('1');
        element(by.css('input[type="password"')).sendKeys(protractor.Key.BACK_SPACE);
        // element(by.css('input[type="submit"')).click();

        expect(element(by.css('.alert-danger')).getText()).toEqual('Password is Required');
        expect(page.location()).toContain('login');
    });
});
