import React from 'react'
import './index.css'
import {createBrowserRouter,RouterProvider} from "react-router-dom";
import { createRoot } from "react-dom/client";
import Layout from './Pages/Shared/Layout.jsx';
import Home from './Pages/Home.jsx';
import { Provider } from 'react-redux';
import store from './store';
import Login from './Pages/Auth/Login.jsx';
import Register from './Pages/Auth/Register.jsx';
import Authenticated from './Pages/Shared/Guard/Authenticated.js';
import NotAuthenticated from './Pages/Shared/Guard/NotAuthenticated.js';
import Logout from './Pages/Auth/Logout.jsx';
import AdminLayout from './Pages/Shared/AdminLayout.jsx';
import Dashboard from './Pages/Admin/Dashboard.jsx';
import IsAdmin from './Pages/Shared/Guard/IsAdmin.js';
import Products from './Pages/Admin/Products/Products.jsx';
import ProductDetails from './Pages/Admin/Products/ProductDetails.jsx';
import EditProduct from './Pages/Admin/Products/EditProduct.jsx';

const router = createBrowserRouter([
  {
    path:"/",
    element:
      <Authenticated>
        <Layout />
      </Authenticated>
    ,
    children:[
      {index:true, element:<Home />}
    ]
  },
  {
    path:"/admin",
    element:
      <IsAdmin>
        <AdminLayout />
      </IsAdmin>
    ,
    children:[
      {index:true, element:<Dashboard />},
      {path:"products", element:<Products />},
      {path:"products/:id", element:<ProductDetails />},
      {path:"products/edit/:id", element:<EditProduct />},
      
    ]
  },
  {
    path:"/auth",
    children:[
      {
        path:"logout",
        element:
        <Authenticated>
          <Logout />
        </Authenticated>
      },
      {
        path:"login",
        element:
        <NotAuthenticated>
          <Login />
        </NotAuthenticated>
      },
      {
        path:"register",
        element:
        <NotAuthenticated>
          <Register />
        </NotAuthenticated>
      }
    ]
  }
])

createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <Provider store={store}>
      <RouterProvider router={router} />
    </Provider>
  </React.StrictMode>,
)
