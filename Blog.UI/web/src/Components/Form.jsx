import '../Styles/forms.css';
import FormsImage from '../utils/img/Writing-room.svg';
import { useState } from "react";

const Form = () => {


    const [show, setshow] = useState(true);

    const ShowHidePassword = () => {
        setshow(!show);
    };


    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [message, setMessage] = useState("jj");

    let handleSubmit = async (e) => {
        e.preventDefault();
        try {
          let res = await fetch("http://localhost:5232/api/User/Login", {
            method: "POST",
            headers: {'Content-type': "application/json; charset = UTF-8"},
            mode: "cors",
            body: JSON.stringify({
              email: email,
              password: password,
            })
          })
          .then(response => response.json())
          .then(json => {console.log(json); json.status === 200 || 204 ? 
            setMessage("Successful") : setMessage("Some error occured");})
          .catch(err => console.log(err));
        } catch (err) {
          console.log(err);
          setMessage("Some error occured");
        }
      };


    return (
        <form className="form" onSubmit={handleSubmit}>
            <div className="content">
                <div className="heading">
                    <h1>Everyone has a story to share...</h1>
                </div>
                <div className="input-box">
                    <div className="input-field input-email">
                        <i className="las la-envelope-open-text"></i>
                        <input required 
                            type="email" 
                            name="email" 
                            value={email} 
                            className='form-email' 
                            placeholder='johndoe@gmail.com' 
                            onChange={(e) => setEmail(e.target.value)} />
                    </div>
                    <div className="input-field input-password">
                        {show ? <i className="las la-low-vision" onClick={ShowHidePassword}></i> : <i className="las la-eye" onClick={ShowHidePassword}></i>}
                        <input required 
                            minLength={8} 
                            type={show ? "password" : "text"} 
                            name="password" 
                            value={password} 
                            className='form-password' 
                            placeholder='Enter an 8 character password'
                            onChange={(e) => setPassword(e.target.value)} />
                    </div>
                </div>
                <div className="button-box">
                    <button type="submit" className="signin cta-button">Sign In</button>
                    <button className="signup cta-button">Sign Up</button>
                </div>
            </div>

            <div className="images">
                <img id='form-img' src={FormsImage} alt="" />
            </div>
        </form>
    )
}

export default Form