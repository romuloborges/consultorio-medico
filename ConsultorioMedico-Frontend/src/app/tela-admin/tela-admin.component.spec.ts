import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TelaAdminComponent } from './tela-admin.component';

describe('TelaAdminComponent', () => {
  let component: TelaAdminComponent;
  let fixture: ComponentFixture<TelaAdminComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TelaAdminComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TelaAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
