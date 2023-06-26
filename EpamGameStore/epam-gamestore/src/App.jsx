import { useState } from 'react'
import reactLogo from './assets/react.svg'
import './App.css'
import NavigationModule from './components/NavigationModule'
import GamesShowcase from './components/GamesShowcase'
import SiteFooter from './components/SiteFooter'


function App() {

  const [scrollState, setScrollState] = useState(0);

  window.scrollLockFunction = (state) => {
    setScrollState(state);
  }

  const scrollStyle = {
    overflow: scrollState ? "hidden" : "auto"
  }

  return (
    <div className="App" style={scrollStyle}>
      <NavigationModule></NavigationModule>
      <GamesShowcase></GamesShowcase>
      <SiteFooter></SiteFooter>
    </div>
  )
}

export default App
