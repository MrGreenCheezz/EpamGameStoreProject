import React, { Component } from 'react'
import './GameItemCard.css'

export default class GameItemCard extends Component {
  constructor(props) {
    super(props)
  
    this.state = {

    }

  }
    render() {
    return (
      <div className='Card'>
        <div className='CardBody'>
            <div className='CardPicture'>
                <img src='https://i.kym-cdn.com/entries/icons/original/000/018/805/getty-baboon.jpg' style={{width: 400, height: 300}}></img>
            </div>
            <div className='CardPrice'>
               $ {this.props.Price}
            </div>
            <div className='CardTitle'>
                {this.props.Title}
            </div>
        </div>
      </div>
    )
  }
}
