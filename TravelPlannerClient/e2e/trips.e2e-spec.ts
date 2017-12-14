import { AppPage } from './app.po';
import { element, by, browser, ElementFinder } from 'protractor';
import { protractor } from 'protractor/built/ptor';
import { async } from '@angular/core/testing';

declare let Zone: any;
describe('All Records Page', () => {
    let page: AppPage;

    beforeAll(done => {
        page = new AppPage();
        page.login();
        page.navigateToAllRecordsPage().then(done);
    });

    beforeEach(() => {
        browser.ignoreSynchronization = true;
    });

    afterAll(() => {
        page.clearStorage();
    });
});