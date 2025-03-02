// Baserad på referens (5) Css Confetti on Click - (RewKun) 
window.confettiEffect = (selector) => {
    function random(max) {
        return Math.random() * max;
    }

    const button = document.querySelector(selector);
    if (!button) return;

    // Check if CSS properties like transform are supported
    const isModern = CSS.supports("animation", "bang 700ms ease-out forwards");

    const fragment = document.createDocumentFragment();
    for (let i = 0; i < 100; i++) {
        const element = document.createElement("i");

        if (isModern) {
            // Modern effect
            element.style.cssText = `
                position: absolute;
                transform: translate3d(${random(500) - 250}px, ${random(200) - 150}px, 0) 
                rotate(${random(360)}deg);
                background: hsla(${random(360)}, 100%, 50%, 1);
                width: 6px;
                height: 6px;
                border-radius: 50%;
                animation: bang 700ms ease-out forwards;
                opacity: 0;
            `;
        } else {
            // Simpler effect for old browsers
            element.style.cssText = `
                position: absolute;
                left: ${random(100)}%;
                top: ${random(100)}%;
                background: red;
                width: 5px;
                height: 5px;
                border-radius: 50%;
                opacity: 0.8;
            `;
        }

        fragment.appendChild(element);
    }

    button.appendChild(fragment);
};

