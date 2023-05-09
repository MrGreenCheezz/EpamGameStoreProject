import React, { Component } from 'react'
import './ComponentsCSS/NavigationModule.css'
import myLogo from '../assets/logo.png'
import AuthPanel from './AuthPanel'

export default class NavigationModule extends Component {
  constructor(props) {
    super(props)
    this.ChangePanelVision = this.ChangePanelVision.bind(this)
    this.ChangePanelMode = this.ChangePanelMode.bind(this)
    this.SignInClicked  =  this.SignInClicked.bind(this)
    this.SignUpClicked  = this.SignUpClicked.bind(this)
    
  
    this.state = {
       IsAuthPanelOpen: false,
       AuthPanelMode: "Register"
    }
  }

  ChangePanelVision(newState){
    this.setState({IsAuthPanelOpen: newState})
  }

  ChangePanelMode(mode){
    this.setState({AuthPanelMode: mode})
  }
  
  SignInClicked(){
    this.ChangePanelMode("Login");
    this.ChangePanelVision(true);
  }

  SignUpClicked(){
    this.ChangePanelMode("Register");
    this.ChangePanelVision(true);
  }

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
          <div className='SignButton'> 
            <b className='text-button' onClick={this.SignInClicked}>Sign in  </b> <b>/</b><b className='text-button' onClick={this.SignUpClicked}>  Sign up</b>
          </div>
        </nav>
        <div className='AuthPanelFading' style={{display: this.state.IsAuthPanelOpen ? 'flex' : 'none'}}>
          <AuthPanel Mode={this.state.AuthPanelMode} HideFunction={this.ChangePanelVision}></AuthPanel>
        </div>
      </div>
    )
  }
}
