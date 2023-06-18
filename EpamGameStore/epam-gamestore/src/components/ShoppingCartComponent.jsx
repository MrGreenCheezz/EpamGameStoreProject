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
        this.FadingMenuClick = this.FadingMenuClick.bind(this);
        this.ChangeItemCount = this.ChangeItemCount.bind(this);
      
        this.state = {
          ItemCount:0,
          Items: [],
          IsCartShowed:false,
          Mounted: false
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

      ChangeItemCount(amount){
        this.setState({ItemCount:this.state.ItemCount + amount});
      }
      


      AddItemEvent(data){
        var arr = this.state.Items.slice();
        var item = this.state.Items.findIndex(element => element.Id === data.Id);
        if(item !== -1){
          arr[item].Count+= 1;
          this.setState({Items:arr,ItemCount:this.state.ItemCount+1});
        }
        else{
          this.setState({ItemCount:this.state.ItemCount+1, Items:[...this.state.Items, data]});
        }
      }
      

      componentWillUnmount(){
        eventBus.remove("ItemAdded");
      }

      FadingMenuClick(event){
        if(event.target.className === 'ShoppingCartMenuFading'){
          this.setState({IsCartShowed: false})
        }
      }
    
      

  render() {
    
    return (
      <div className='CartSection'>
        <div style={{textAlign:"center"}}>{this.state.ItemCount}</div>
        <img src={cart} onClick={() => this.ChangeCartVisibility(!this.state.isVisible)} style={{width:"35px",height:"35px"}}></img>
        <div className='ShoppingCartMenuFading' style={{ display: this.state.IsCartShowed ? 'flex' : 'none' }} onClick={this.FadingMenuClick}>
            <ShoppingCartMenu Showed={this.state.IsCartShowed} ChangeVisibilityFunction={this.ChangeCartVisibility} CartItems={this.state.Items} ChangeItemCount={this.ChangeItemCount}></ShoppingCartMenu>
          </div>
      </div>
    )
  }
}
