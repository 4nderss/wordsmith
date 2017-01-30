import {PageObject_App} from './app.po';
import {PageObject_Start} from './start.po';

import {browser, element, by, By, $, $$, ExpectedConditions} from 'aurelia-protractor-plugin/protractor';

describe('Wordsmith application', function() {
    let po_app: PageObject_App;
    let po_start: PageObject_Start;


    beforeAll(() => {
        po_app = new PageObject_App();
        po_start = new PageObject_Start();
        browser.loadAndWaitForAureliaPage(`http://localhost:19876`);
    });
 

  it('should load the page and display the initial page title', () => {
      expect(po_app.getCurrentPageTitle()).toBe('Wordsmith Inc');
  });

  it('should display greeting', () => {
      expect(po_start.getGreeting()).toBe('YOUR FAVORITE SOURCE FOR TRANSFORMING SENTENCES');
  });

 
  it('should display a Try It button', () => {
      element(by.xpath("//div/a[contains(.,'Try it')]")).click();
  });

  it('should contain a textarea for sentence input', () => {
      expect(po_start.getSentenceInput().isPresent()).toBeTruthy();
      expect(po_start.getSentenceInput().isDisplayed()).toBeTruthy();
      
  });

  it('should work to write text in the textarea', () => {
      po_start.setSentenceInput("Testing Sentence");

      //let value = po_start.getSentenceInputValue();
      //expect(value).toBe("Testing Sentence");

  });

  it('should work to press the transform button', () => {
       po_start.pressTransformButton();
       browser.sleep(5000); //simulate server wait
  });
 
  it('should display a transformed sentence', () => {
     let actual = po_start.getTransformedSentenceValue();
     expect(actual).toBe("gnitseT ecnetneS");
  });
 
});
