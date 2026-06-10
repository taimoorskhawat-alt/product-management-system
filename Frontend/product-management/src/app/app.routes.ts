import { Routes } from '@angular/router';
import { Home } from './pages/home/home';
import { Products } from './pages/products/products';

import { AddProduct } from './pages/add-product/add-product';
import { EditProducts } from './pages/edit-products/edit-products';

import { About } from './pages/about/about';
import { Contact } from './pages/contact/contact';
import { Login,} from './pages/login/login';
import { authGuard } from './auth-guard';
import { Register } from './pages/register/register';

export const routes: Routes = [
    { path: '', redirectTo:'login', pathMatch:'full' },
    { path: 'home', component:Home },
    { path: 'products', component: Products,canActivate:[authGuard] },
    { path: 'add-product', component: AddProduct,canActivate:[authGuard] },
    { path: 'edit-product/:id', component: EditProducts,canActivate:[authGuard] },
    {path:'login',component:Login},
    { path: 'about', component: About },
    { path: 'contact', component: Contact },
    {path:'register',component:Register}

]
