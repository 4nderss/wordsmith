import {PageObject_App} from './app.po';
import {PageObject_Start} from './start.po';

import {browser, element, by, By, $, $$, ExpectedConditions} from 'aurelia-protractor-plugin/protractor';

describe('Wordsmith application', function() {
    let po_app: PageObject_App;
    let po_start: PageObject_Start;

  beforeEach( () => {
      po_app = new PageObject_App();
      po_start = new PageObject_Start();
      browser.loadAndWaitForAureliaPage(`http://localhost:19876`);
  });

  it('should load the page and display the initial page title', () => {
      expect(po_app.getCurrentPageTitle()).toBe('Wordsmith Inc');
  });

  it('should display greeting', () => {
      //expect(PageObject_Start.getGreeting()).toBe('Welcome to the Aurelia Navigation App');
  });
    
 
});
