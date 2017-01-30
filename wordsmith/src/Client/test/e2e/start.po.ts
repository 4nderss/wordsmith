import {browser, element, by, By, $, $$, ExpectedConditions} from 'aurelia-protractor-plugin/protractor';

export class PageObject_Start {
  getGreeting() {
      return element(by.id('homeHeading')).getText();
  } 

  pressTransformButton() {
      return element(by.id('TransformBtn')).click();
  }  

  getSentenceInput() {
      return element(by.id('SentenceInput'));
  }  

  setSentenceInput(val) {
      element(by.id('SentenceInput')).sendKeys(val);

      browser.sleep(1000);
  }


  getSentenceInputValue() {
      return element(by.id('SentenceInput')).getText();
  }

  getTransformedSentenceValue() {
      return element(by.id('TransformedSentence')).getText();

  }
  
}
