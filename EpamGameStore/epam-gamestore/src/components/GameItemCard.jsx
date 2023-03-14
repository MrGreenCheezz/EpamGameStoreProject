import React, { Component } from 'react'
import './/ComponentsCSS/GameItemCard.css'

export default class GameItemCard extends Component {
  constructor(props) {
    super(props)

    this.state = {

    }

  }
  render() {
    return (
      <div className="card text-bg-dark Custom" style={{width:400, height:"fit-content", marginTop: 30}}>
        <img src="https://media.npr.org/assets/img/2017/09/12/macaca_nigra_self-portrait-3e0070aa19a7fe36e802253048411a38f14a79f8-s1100-c50.jpg" className="card-img" alt="..." style={{width:"100%"}}></img>
        <div className="card-img-overlay custom-bg">
          <h5 className="card-title">$ {this.props.Price}</h5>
          <p className="card-text">{this.props.Title}</p>
        </div>
      </div>
    )
  }
}
