import React, { Component } from 'react'
import { ThemeConsumer } from 'react-bootstrap/esm/ThemeProvider';
import "./ComponentsCSS/ShoppingCartMenu.css"

export default class ShoppingCartMenu extends Component {
  constructor(props) {
    super(props)
    this.EditElementOnIndex = this.EditElementOnIndex.bind(this);

    this.state = {
      IsShowed: this.props.Showed,
      Items: this.props.CartItems,
      TotalPrice: 0,
      CartMode: "Cart"
    }
  }




  componentDidUpdate(prevProps, prevState) {
    if (prevProps.CartItems != this.props.CartItems) {
      var newPrice = 0;
      this.props.CartItems.forEach((element, index) => {
        newPrice += element.Price * element.Count;
      });
      this.setState({ TotalPrice: newPrice, Items: this.props.CartItems });
    }
  }

  EditElementOnIndex(index, amount) {
    this.props.ChangeItemCount(amount);
    var arr = this.state.Items.slice();
    if ((arr[index].Count + amount) == 0) {
      arr.splice(index, 1);
    }
    else {
      arr[index].Count += amount;
    }
    this.setState({ Items: arr });
    var newPrice = 0;
    arr.forEach((element, index) => {
      newPrice += element.Price * element.Count;
    });
    this.setState({ TotalPrice: newPrice });
  }

  render() {
    switch (this.state.CartMode) {
      case "Cart":
        {
          return (
            <div className="CartMainForm">
              <div style={{ display: "flex", height: 50, alignItems: "center" }}>
                <div style={{ flexGrow: 1, fontSize: 30 }}>Your Cart:</div>
                <div style={{ fontSize: 25, marginRight: 40 }}>Total Price: {this.state.TotalPrice}$</div>
                <button type="button" className="btn btn-success" onClick={() => this.setState({ CartMode: "Checkout" })}>Proceed</button>
              </div>

              <div style={{ width: "99%" }}>
                {this.state.Items.map((item, index) => {
                  return (
                    <div key={index} style={{ borderColor: "black", marginBottom: 15, borderStyle: "solid" }} >
                      <div style={{ display: "flex", alignItems: "center" }}>
                        <img src={item.ImageUrl} ></img>
                        <div style={{ flexGrow: 1, textAlign: "center", width: "25%" }}>
                          <div style={{ fontSize: 25 }}>{item.Name}</div>
                          <div style={{ fontSize: 25 }}>${item.Price}</div>
                        </div>
                        <div style={{ display: "flex", alignItems: "center", flexGrow: 1 }}>
                          <div style={{ flexGrow: 1, textAlign: "center", fontSize: 50 }} onClick={() => this.EditElementOnIndex(index, -1)}>-</div>
                          <div style={{ flexGrow: 1, textAlign: "center", fontSize: 25 }}>{item.Count}</div>
                          <div style={{ flexGrow: 1, textAlign: "center", fontSize: 50 }} onClick={() => this.EditElementOnIndex(index, 1)}>+</div>
                        </div>
                        <div style={{ display: "flex", alignItems: "center", flexGrow: 1 }}>Product price: ${this.state.Items[index].Count * this.state.Items[index].Price}</div>
                        <img src="https://www.iconpacks.net/icons/1/free-trash-icon-969-thumb.png" style={{ height: 50, width: 50 }}
                          onClick={() => this.EditElementOnIndex(index, this.state.Items[index].Count * -1)}></img>
                      </div>
                    </div>
                  )
                })}
              </div>
            </div>
          )
        }
      case "Checkout":
        {
          return (
            <div className='CartCheckoutForm'>
              <div style={{ display: "flex", height: 50, alignItems: "center" }}>
                <img src="https://www.pngmart.com/files/16/Left-Arrow-Icon-PNG-Transparent-Image.png" style={{ height: 25, width: 25, marginRight: 10 }} onClick={()=>
              this.setState({CartMode:"Cart"})}></img>
              <div style={{ flexGrow: 1, fontSize: 30, color:"black" }}>Completing your order</div>
              </div>
              <form className='MainForm' style={{display:"flex",flexDirection:"column"}}>
                <div>
                  <label htmlFor="fname">First name: </label><br></br>
                  <input type="text" value={this.state.Email} id="fname" name="fname" onChange={(event) => { this.setState({ Email: event.target.value }) }}></input><br></br>
                </div>
                <div>
                  <label htmlFor="lname">Last name: </label><br></br>
                  <input type="text" id="lname" value={this.state.Password} name="lname" onChange={(event) => { this.setState({ Password: event.target.value }) }}></input><br></br>
                </div>
                <div>
                  <label htmlFor="fname">Email: </label><br></br>
                  <input type="text" value={this.state.Email} id="fname" name="fname" onChange={(event) => { this.setState({ Email: event.target.value }) }}></input><br></br>
                </div>
                <div>
                  <label htmlFor="lname">Phone: </label><br></br>
                  <input type="text" id="lname" value={this.state.Password} name="lname" onChange={(event) => { this.setState({ Password: event.target.value }) }}></input><br></br>
                </div>
                <div>
                  <label htmlFor="paymentMethod">Payment type: </label><br></br>
                  <select id="paymentMethod" name="paymentMethod" style={{width: "100%"}}>
                    <option value="card">Card</option>
                    <option value="cash">Cash</option>
                  </select><br></br>
                </div>
                <div>
                  <label htmlFor="comments">Comments: </label><br></br>
                  <textarea name="comments" rows="10" cols="30" style={{width:"100%", resize: "none", height: 150}}></textarea><br></br>
                </div>
                <button type="button" className="btn btn-success" style={{width:"35%", height:35, alignSelf:"center", marginBottom: 5}}>Order!</button>
              </form>
            </div>
          )

        }
    }

  }
}
