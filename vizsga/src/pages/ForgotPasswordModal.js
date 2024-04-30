import React from 'react'

//0, 1, 2
const CurrentForgotPasswordState = ({ changeState, setChangeState }) => {
    switch (changeState) {
        case 0:
            return <PasswordChangeRequestContent />
        case 1:
            return <PasswordChangeVerifyContent />
        case 2:
            return <PasswordChangeContent />
        default:
            break;
    }
}

const PasswordChangeRequestContent = () => {
    return (
        <form className='form'>
            <div className="mb-3">
                <label htmlFor="emailForg" className="form-label">Fiókodnál használt email megadása</label>
                <input type="text" className="form-control" id="emailForg" aria-describedby="emailHelp" />
            </div>
        </form>
    );
}
const PasswordChangeVerifyContent = () => {
    return (
        <form className='form'>
            <div className="mb-3">
                <label htmlFor="emailVer" className="form-label">Megerősítő kód megadása</label>
                <input type="text" className="form-control" id="emailVer" aria-describedby="emailVerHelp" />
            </div>
        </form>
    );
}
const PasswordChangeContent = () => {
    return (
        <form className='form'>
            <p>Új jelszó beállítása</p>
            <hr />
            <div className="mb-3">
                <label htmlFor="pass" className="form-label">Új jelszó</label>
                <input type="password" className="form-control" id="pass" aria-describedby="passHelp" />
            </div>
            <div className="mb-3">
                <label htmlFor="passRep" className="form-label">Új jelszó megerősítés</label>
                <input type="password" className="form-control" id="passRep" aria-describedby="passRepHelp" />
            </div>
        </form>
    );
}

var fadeTimeout;
const fadeTime = 3 * 1000;

const AlertContent = ({alertId, classList, content}) => {
    return (
        <div id={alertId} className={classList} role="alert">
            {content}
        </div>
    );
}

const ForgotPasswordModal = () => {
    const [changeState, setChangeState] = React.useState(0);
    const [alertMessage, setAlertMessage] = React.useState("A megerésítő költőt elküldtuk emailre!");
    return (
        <div>
            <div className="modal fade" id="forgotModal" tabIndex="-1" aria-labelledby="forgotModalLabel" aria-hidden="true">
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h1 className="modal-title fs-5" id="forgotModalLabel">Elfelejtett jelszó</h1>
                            <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                            <CurrentForgotPasswordState changeState={changeState} setChangeState={setChangeState} />
                        </div>
                        <div className="modal-footer">
                            <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">Mégsem</button>
                            <button id="changeButton" onClick={async () => {
                                let changeButton = document.getElementById("changeButton");
                                let alert = document.getElementById("alert");
                                let email = document.getElementById("emailForg");
                                let emailVer = document.getElementById("emailVer");

                                alert.classList.remove("alert-danger");
                                alert.classList.remove("show");
                                fadeTimeout = setTimeout(async () => {
                                    document.getElementById("alert").classList.remove("show");
                                }, fadeTime);
                                
                                if(changeState === 0) {
                                    alert.classList.add("show");
                                }

                                if(email != null && email.value === "") {
                                    setAlertMessage("Add meg a fiókodhoz használt emailt!");
                                    alert.classList.add("alert-danger");
                                    alert.classList.add("show");
                                    return;
                                } else {
                                    setAlertMessage("A megerősítő kódot elküldtük emailre!");
                                }

                                if(emailVer != null && emailVer.value === "") {
                                    setAlertMessage("Megerősítő kód megadása szükséges!");
                                    alert.classList.add("alert-danger");
                                    alert.classList.add("show");
                                    return;
                                }


                                if (changeState === 0) {
                                    setChangeState(1);
                                    changeButton.textContent = "Új jelszó kérése";
                                    
                                }
                                if (changeState === 1) {
                                    setChangeState(2);
                                    changeButton.textContent = "Jelszó megváltoztatása";
                                    changeButton.setAttribute("data-bs-dismiss", "modal");
                                    return;
                                }
                                if (changeState === 2) {
                                    setChangeState(0);
                                    changeButton.textContent = "Megerősítő kód kérés";
                                    return;
                                }

                                const forgotModal = document.getElementById('forgotModal');
                                forgotModal.addEventListener('hidden.bs.modal', event => {
                                    setChangeState(0);
                                    changeButton.textContent = "Megerésítő kód kérés";
                                    changeButton.removeAttribute("data-bs-dismiss");
                                    window.clearTimeout(fadeTimeout);
                                    
                                })

                            }} type="button" className="btn btn-primary">Megerősítő kód kérés</button>
                        </div>
                    </div>
                    <AlertContent alertId="alert" classList="alert alert-success alert-dismissible fade mt-2" content={alertMessage} />
                </div>
            </div>
        </div>

    )
}

export default ForgotPasswordModal