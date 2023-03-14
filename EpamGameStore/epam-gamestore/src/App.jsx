import { useState } from 'react'
import reactLogo from './assets/react.svg'
import './App.css'
import NavigationModule from './components/NavigationModule'
import GamesShowcase from './components/GamesShowcase'
import SiteFooter from './components/SiteFooter'


function App() {

  return (
    <div className="App">
      <NavigationModule></NavigationModule>
      <GamesShowcase></GamesShowcase>
      <SiteFooter></SiteFooter>
    </div>
  )
}

export default App
