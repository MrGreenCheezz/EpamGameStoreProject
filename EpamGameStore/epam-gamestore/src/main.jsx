import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App'
import { BrowserRouter, Routes, Route } from "react-router-dom";
import 'bootstrap/dist/css/bootstrap.min.css';
import './index.css'
import GameFullPage from './GameFullPage';



ReactDOM.createRoot(document.getElementById('root')).render(

    <BrowserRouter>
    <Routes>
      <Route path='/' element={<App></App>}></Route>
      <Route path='/game/:id' element={<GameFullPage/>}></Route>
    </Routes>
    </BrowserRouter>
,
)
