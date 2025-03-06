document.addEventListener('DOMContentLoaded', () => {
    console.log("✅ DOM fully loaded and parsed!");

    const darkModeSwitch = document.querySelector('#darkmode-switch');

    // Prevent errors if the element is missing
    if (!darkModeSwitch) {
        console.error("❌ Dark mode switch not found!");
        return;
    }

    console.log("🔘 Dark mode switch found!");

    const hasSetDarkMode = localStorage.getItem('darkmode');
    console.log("🗄 Stored dark mode preference:", hasSetDarkMode);

    if (hasSetDarkMode === null) {
        console.log("🌍 No preference set, checking system preference...");
        if (window.matchMedia('(prefers-color-scheme: dark)').matches) {
            console.log("🌙 System prefers dark mode, enabling...");
            enableDarkMode();
        } else {
            console.log("☀️ System prefers light mode, disabling...");
            disableDarkMode();
        }
    } else if (hasSetDarkMode === 'on') {
        console.log("🌙 User preference: Dark mode enabled.");
        enableDarkMode();
    } else if (hasSetDarkMode === 'off') {
        console.log("☀️ User preference: Dark mode disabled.");
        disableDarkMode();
    }

    darkModeSwitch.addEventListener('change', () => {
        console.log("🔄 Toggle switched:", darkModeSwitch.checked);
        if (darkModeSwitch.checked) {
            console.log("🌙 Enabling dark mode...");
            enableDarkMode();
        } else {
            console.log("☀️ Disabling dark mode...");
            disableDarkMode();
        }
    });

    function enableDarkMode() {
        console.log("🌙 Dark mode activated!");
        darkModeSwitch.checked = true;
        document.documentElement.classList.add('dark');
        localStorage.setItem('darkmode', 'on');
        console.log("💾 Saved to localStorage: darkmode = 'on'");
    }

    function disableDarkMode() {
        console.log("☀️ Dark mode deactivated!");
        darkModeSwitch.checked = false;
        document.documentElement.classList.remove('dark');
        localStorage.setItem('darkmode', 'off');
        console.log("💾 Saved to localStorage: darkmode = 'off'");
    }
});


