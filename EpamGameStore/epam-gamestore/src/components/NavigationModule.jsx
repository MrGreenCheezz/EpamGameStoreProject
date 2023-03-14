import React, { Component } from 'react'
import './ComponentsCSS/NavigationModule.css'
import myLogo from '../assets/logo.png'

export default class NavigationModule extends Component {
  render() {
    return (
      <div>
        <nav class="navbar navbar-expand-lg bg-body-tertiary bg-black">
          <div class="container-fluid">
            <a class="navbar-brand text-light" href="/">
            <img src={myLogo} style={{width: 30}}></img>
            GameStoreProject
            </a>
            <button class="navbar-toggler " type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
              <span class="navbar-toggler-icon "></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
              <ul class="navbar-nav">
                <li class="nav-item">
                  <a class="nav-link active text-light" aria-current="page" href="/">Games</a>
                </li>
                <li class="nav-item">
                  <a class="nav-link  text-light" href="#">Community</a>
                </li>
                <li class="nav-item">
                  <a class="nav-link  text-light" href="#">About</a>
                </li>
                <li class="nav-item">
                  <a class="nav-link  text-light">Support</a>
                </li>
              </ul>
            </div>
          </div>
        </nav>
      </div>
    )
  }
}
