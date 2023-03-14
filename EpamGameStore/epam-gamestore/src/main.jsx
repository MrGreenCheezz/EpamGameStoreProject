import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App'
import GamesShowcase from './components/GamesShowcase'
import 'bootstrap/dist/css/bootstrap.min.css';
import './index.css'
import NavigationModule from './components/NavigationModule';
import SiteFooter from './components/SiteFooter';

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <NavigationModule></NavigationModule>
    <GamesShowcase></GamesShowcase>
    <SiteFooter></SiteFooter>
  </React.StrictMode>,
)
