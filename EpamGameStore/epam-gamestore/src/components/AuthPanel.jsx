import React, { Component } from 'react'
import './ComponentsCSS/AuthPanel.css'

export class AuthPanel extends Component {
    constructor(props) {
        super(props)
        this.SendLogin = this.SendLogin.bind(this);
        this.SendRegister = this.SendRegister.bind(this);
        this.HideFunction = this.props.HideFunction;

        this.state = {
            Email: "",
            Password: "",
            FirstName: "",
            SecondName: "",
            UserName: "",
            ErrorString: ""
        }
    }

    async SendLogin() {
        fetch('http://localhost:21409/api/auth/login?email=' + this.state.Email + '&password=' + this.state.Password, {
            method: 'POST',
            credentials: 'include'
        }).then((response) => response.json()).then((data) => {
            if(data.status != 200){
                this.setState({ErrorString: data.detail})
            }
            else
            {
                this.setState({
                    Email: "",
                    Password: "",
                    FirstName: "",
                    SecondName: "",
                    UserName: ""
                })
                this.HideFunction(false);
            }    
        })


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
                UserName: ""
            })
            this.HideFunction(false);
        }               
    }

    render() {
        switch (this.props.Mode) {
            case "Login": {
                return (
                    <div className='MainPanel'>
                        <div className='ExitButton'>
                            <i className='bi-x-circle' onClick={()=> this.HideFunction(false)}></i>
                        </div>
                        <p style={{ textAlign: 'center', fontSize: '40px' }}>Login:</p>
                        <p style={{color:'red'}}>{this.state.ErrorString}</p>
                        <form className='MainForm'>
                            <label htmlFor="fname">Email: </label><br></br>
                            <input type="text" id="fname" name="fname" onChange={(event) => { this.setState({ Email: event.target.value }) }}></input><br></br>
                            <label htmlFor="lname">Password: </label><br></br>
                            <input type="text" id="lname" name="lname" onChange={(event) => { this.setState({ Password: event.target.value }) }}></input><br></br>
                            <button className='btn btn-success' onClick={this.SendLogin}>Submit</button>
                        </form>
                    </div>
                )
            }

            case "Register": {
                return (
                    <div className='MainPanel'>
                        <div className='ExitButton'>
                            <i className='bi-x-circle' onClick={()=> this.HideFunction(false)}></i>
                        </div>
                        <p style={{ textAlign: 'center', fontSize: '40px' }}>Register:</p>
                        <p style={{color:'red'}}>{this.state.ErrorString}</p>
                        <form className='MainForm'>
                            <label htmlFor="fname" >First name:</label><br></br>
                            <input type="text" id="fname" name="fname" onChange={(event) => { this.setState({ FirstName: event.target.value }) }}></input><br></br>
                            <label htmlFor="lname">Last name:</label><br></br>
                            <input type="text" id="lname" name="lname" onChange={(event) => { this.setState({ SecondName: event.target.value }) }}></input><br></br>
                            <label htmlFor="uname">User name:</label><br></br>
                            <input type="text" id="uname" name="lname" onChange={(event) => { this.setState({ UserName: event.target.value }) }}></input><br></br>
                            <label htmlFor="email">Email:</label><br></br>
                            <input type="text" id="email" name="lname" onChange={(event) => { this.setState({ Email: event.target.value }) }}></input><br></br>
                            <label htmlFor="pass">Password:</label><br></br>
                            <input type="text" id="pass" name="lname" onChange={(event) => { this.setState({ Password: event.target.value }) }}></input><br></br>
                            <button className='btn btn-success' onClick={this.SendRegister}>Submit</button>
                        </form>
                    </div>
                )
            }
        }
    }

}

export default AuthPanel