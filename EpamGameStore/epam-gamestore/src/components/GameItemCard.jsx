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
      <div className="card text-bg-dark Custom" style={{width:"fit-content", height:"fit-content", marginTop: 30}}>
        <img src={this.props.ImageUrl} className="card-img" alt="..." style={{width:"fit-content",height: 300}}></img>
        <div className="card-img-overlay custom-bg">
        <a className="card-text" style={{fontSize:20}} href={"/game/"+this.props.Id}>{this.props.Title}</a>
          <h5 className="card-title CardPrice">$ {this.props.Price}</h5>
        </div>
      </div>
    )
  }
}
