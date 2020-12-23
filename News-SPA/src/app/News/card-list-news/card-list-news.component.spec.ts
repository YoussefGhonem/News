import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CardListNewsComponent } from './card-list-news.component';

describe('CardListNewsComponent', () => {
  let component: CardListNewsComponent;
  let fixture: ComponentFixture<CardListNewsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CardListNewsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CardListNewsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
