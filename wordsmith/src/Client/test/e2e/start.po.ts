import {browser, element, by, By, $, $$, ExpectedConditions} from 'aurelia-protractor-plugin/protractor';

export class PageObject_Start {
  getGreeting() {
    return element(by.tagName('h2')).getText();
  } 

  pressTransformButton() {
    return element(by.css('button[type="submit"]')).click();
  }  
  
}
