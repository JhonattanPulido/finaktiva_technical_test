import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LogsComponent } from './components/logs/logs.component';

const routes: Routes = [
  {
    path: 'logs',
    component: LogsComponent
  },
  {
    path: '',
    redirectTo: 'logs',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
