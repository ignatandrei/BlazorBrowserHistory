// This is a JavaScript module that is loaded on demand. It can export any number of
// functions, and may import other JavaScript modules if required.

export function showPrompt(message) {
    console.log('Showing prompt with message: ' + message);
    console.log('Current document title is: ' + document.title);
    return prompt(message, 'Type anything here');
};


export function documentTitle() {
    return document.title;
};

export function MyDocumentTitle(a) {
    return "andrei";// document.title;
};