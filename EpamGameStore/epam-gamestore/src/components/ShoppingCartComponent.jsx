import React, { Component } from 'react';
import eventBus  from '../eventBus'
import './ComponentsCSS/ShoppingCart.css'
import cart from '../assets/shopCart.png'
import ShoppingCartMenu from './ShoppingCartMenu';

export default class ShoppingCartComponent extends Component {
    constructor(props) {
        super(props)
        this.AddItemEvent = this.AddItemEvent.bind(this);
        this.ChangeCartVisibility = this.ChangeCartVisibility.bind(this);
      
        this.state = {
          ItemCount:0,
          Items: [],
          IsCartShowed:false
        }
      }

      componentDidMount(){
        eventBus.on('ItemAdded',(data)=>{
            this.AddItemEvent(data);
        });
      }

      ChangeCartVisibility(isVisible){
        this.setState({IsCartShowed: isVisible})
      }

      AddItemEvent(data){
        this.setState({ItemCount:this.state.ItemCount+1, Items:[...this.state.Items, data]});
      }

      componentWillUnmount(){
        eventBus.remove("ItemAdded");
      }

  render() {
    
    return (
      <div className='CartSection'>
        <div style={{textAlign:"center"}}>{this.state.ItemCount}</div>
        <img src={cart}></img>
        <div className='ShoppingCartMenuFading' style={{ display: this.state.IsCartShowed ? 'flex' : 'none' }}>
            <ShoppingCartMenu Showed={this.state.IsCartShowed} ChangeVisibilityFunction={this.ChangeCartVisibility} CartItems={this.state.Items}></ShoppingCartMenu>
          </div>
      </div>
    )
  }
}
