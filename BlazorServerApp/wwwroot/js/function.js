export function showAlert(message) {
    alert(message);
}

// Exporting a function that returns a value
export function getCurrentDateTime() {
    return new Date().toLocaleString();
}

//Parameteize funvtion
export function message(message, count) {
    alert(`Message: ${message}, Count: ${count}`);
}

export window.blazorCulture = {
    get: () => window.localStorage['BlazorCulture'],
    set: (value) => window.localStorage['BlazorCulture'] = value
}

export function ChangeCulture(string culture) {

}

//DotNet Object Reference
export function interactWithDotNetObject(dotNetObject) {
    // Call a .NET method from the object reference
    dotNetObject.invokeMethodAsync('DotNetMethod', 'Hello from JavaScript!').then(response => {
        console.log('Response from .NET:', response);
    });

    // Accessing a property
    console.log('Accessed .NET Property:', dotNetObject.someProperty);
}