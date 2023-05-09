import React, { Component } from 'react'
import './ComponentsCSS/NavigationModule.css'
import myLogo from '../assets/logo.png'

export default class NavigationModule extends Component {
  render() {
    return (
      <div>
        <nav className="navbar navbar-expand-lg bg-body-tertiary bg-black">
          <div className="container-fluid">
            <a className="navbar-brand text-light" href="/">
            <img src={myLogo} style={{width: 30}}></img>
            GameStoreProject
            </a>
            <button className="navbar-toggler " type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
              <span className="navbar-toggler-icon "></span>
            </button>
            <div className="collapse navbar-collapse" id="navbarNav">
              <ul className="navbar-nav">
                <li className="nav-item">
                  <a className="nav-link active text-light" aria-current="page" href="/">Games</a>
                </li>
                <li className="nav-item">
                  <a className="nav-link  text-light" href="#">Community</a>
                </li>
                <li className="nav-item">
                  <a className="nav-link  text-light" href="#">About</a>
                </li>
                <li className="nav-item">
                  <a className="nav-link  text-light">Support</a>
                </li>
              </ul>
            </div>
          </div>
        </nav>
      </div>
    )
  }
}
