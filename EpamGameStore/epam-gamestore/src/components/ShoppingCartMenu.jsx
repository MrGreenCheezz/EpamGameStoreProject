import React, { Component } from 'react'

export default class ShoppingCartMenu extends Component {
    constructor(props) {
      super(props)
    
      this.state = {
         IsShowed: this.props.Showed,
         Items: this.props.CartItems
      }
    }

    

  render() {
    return (
      <div>
        <div>
            {this.props.CartItems.map(item => {
                return(
                    <div>
                        <div>{item.Name}</div>
                        <div>{item.Price}</div>
                        <img src={item.ImageUrl}></img>
                    </div>
                )
                })}
        </div>
      </div>
    )
  }
}
