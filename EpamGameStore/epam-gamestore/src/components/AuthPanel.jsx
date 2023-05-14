import React, { Component } from 'react'
import './ComponentsCSS/AuthPanel.css'

export class AuthPanel extends Component {
    constructor(props) {
        super(props)
        this.SendLogin = this.SendLogin.bind(this);
        this.SendRegister = this.SendRegister.bind(this);
        this.closePanel = this.closePanel.bind(this);
        this.HideFunction = this.props.HideFunction;

        this.state = {
            Email: "",
            Password: "",
            FirstName: "",
            SecondName: "",
            UserName: "",
            ErrorString: "",
            NeedToRemember: false
        }
    }

    closePanel(){
        this.HideFunction(false);
        this.setState({
            Email: "",
            Password: "",
            FirstName: "",
            SecondName: "",
            UserName: "",
            ErrorString: "",
            NeedToRemember: false
        })
    }

    componentDidMount(){
        if(localStorage.getItem("UserEmail") != null){
            this.setState({
                Email: localStorage.getItem("UserEmail"),
                Password: localStorage.getItem("UserPassword"),
                NeedToRemember: true
            })
        }
    }
    

    async SendLogin() {
        if(this.state.NeedToRemember){
            localStorage.setItem("UserEmail", this.state.Email)
            localStorage.setItem("UserPassword", this.state.Password)
        }
        else{
            localStorage.removeItem("UserEmail")
            localStorage.removeItem("UserPassword")
        }        
      var data = await fetch('http://localhost:21409/api/auth/login?email=' + this.state.Email + '&password=' + this.state.Password, {
            method: 'POST',
            credentials: 'include'
        })
            if(data.status != 200){
                var respo = await data.json();
                this.setState({ErrorString: respo.detail})
            }
            else
            {
                this.setState({
                    Email: "",
                    Password: "",
                    FirstName: "",
                    SecondName: "",
                    UserName: "",
                    NeedToRemember: false
                })
                this.HideFunction(false);
                this.props.SetAuthorized(true);
            }    
    }

    async SendRegister(event) {
        event.preventDefault();
       var respo = await fetch('http://localhost:21409/api/auth/register?firstName=' + this.state.FirstName + '&lastName=' + this.state.SecondName +
            '&userName=' + this.state.UserName + '&email=' + this.state.Email + '&password=' + this.state.Password, {
            method: 'POST'
        })
        if(respo.status != 200){
            respo = await respo.json();
            this.setState({ErrorString: respo.detail})
        }
        else
        {
            this.setState({
                Email: "",
                Password: "",
                FirstName: "",
                SecondName: "",
                UserName: "",
                NeedToRemember: false
            })
            this.props.SetAuthorized(true);
            this.HideFunction(false);
        }               
    }

    componentDidUpdate(prevProps){
        if(prevProps.IsVisible != this.props.IsVisible){
            if(localStorage.getItem("UserEmail") != null){
                this.setState({
                    Email: localStorage.getItem("UserEmail"),
                    Password: localStorage.getItem("UserPassword"),
                    NeedToRemember: true
                })
            }
        }
    }

    render() {
        switch (this.props.Mode) {
            case "Login": {
                return (
                    <div className='MainPanel'>
                        <div className='ExitButton'>
                            <i className='bi-x-circle' onClick={()=> this.closePanel()}></i>
                        </div>
                        <p style={{ textAlign: 'center', fontSize: '40px' }}>Login:</p>
                        <p style={{color:'red'}}>{this.state.ErrorString}</p>
                        <form className='MainForm'>
                            <label htmlFor="fname">Email: </label><br></br>
                            <input type="text" value={this.state.Email} id="fname" name="fname" onChange={(event) => { this.setState({ Email: event.target.value }) }}></input><br></br>
                            <label htmlFor="lname">Password: </label><br></br>
                            <input type="text" id="lname" value={this.state.Password} name="lname" onChange={(event) => { this.setState({ Password: event.target.value }) }}></input><br></br>   
                            <input type="checkbox" id="check" name="remember" checked={this.state.NeedToRemember} style={{width:"fit-content", height: "auto"}} onChange={(event) => { this.setState({ NeedToRemember: event.target.checked }) }}></input>
                            <label htmlFor="check">Remember me</label><br></br>                        
                        </form>
                        <button className='btn btn-success' onClick={this.SendLogin}>Submit</button>
                    </div>
                )
            }

            case "Register": {
                return (
                    <div className='MainPanel'>
                        <div className='ExitButton'>
                            <i className='bi-x-circle' onClick={()=> this.closePanel()}></i>
                        </div>
                        <p style={{ textAlign: 'center', fontSize: '40px' }}>Register:</p>
                        <p style={{color:'red'}}>{this.state.ErrorString}</p>
                        <form className='MainForm'>
                            <label htmlFor="fname" >First name:</label><br></br>
                            <input type="text" value={this.state.FirstName} id="fname" name="fname" onChange={(event) => { this.setState({ FirstName: event.target.value }) }}></input><br></br>
                            <label htmlFor="lname">Last name:</label><br></br>
                            <input type="text"value={this.state.SecondName} id="lname" name="lname" onChange={(event) => { this.setState({ SecondName: event.target.value }) }}></input><br></br>
                            <label htmlFor="uname">User name:</label><br></br>
                            <input type="text" value={this.state.UserName} id="uname" name="lname" onChange={(event) => { this.setState({ UserName: event.target.value }) }}></input><br></br>
                            <label htmlFor="email">Email:</label><br></br>
                            <input type="text" value={this.state.Email} id="email" name="lname" onChange={(event) => { this.setState({ Email: event.target.value }) }}></input><br></br>
                            <label htmlFor="pass">Password:</label><br></br>
                            <input type="text" value={this.state.Password} id="pass" name="lname" onChange={(event) => { this.setState({ Password: event.target.value }) }}></input><br></br>
                        </form>
                        <button className='btn btn-success' onClick={this.SendRegister}>Submit</button>
                    </div>
                )
            }
        }
    }

}

export default AuthPanel