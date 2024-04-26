import time
from selenium.webdriver.common.by import By

from selenium import webdriver


driver = webdriver.Chrome()

driver.get("http://localhost:3000/")
driver.maximize_window()
time.sleep(3)

# A főoldal betöltődik bejelentkezés nélkül
login_button = driver.find_element(by=By.XPATH, value="/html/body/div/div/div/div/a[1]")
login_button.click()
time.sleep(3)
driver.save_screenshot("login_page.png");
#-------------------------------------------------


#---- Bejelentkezés tesztelése ----
#1. Felhasznélónév kitöltés
username_field = driver.find_element(by=By.XPATH, value="/html/body/div/div/div/form/div[2]/input")
username_field.send_keys("vitya0717")
#2. Jelszó kitöltés
password_field = driver.find_element(by=By.XPATH, value="/html/body/div/div/div/form/div[3]/input")
password_field.send_keys("Alma123@")
driver.save_screenshot("filled_fields.png")
#3. Bejelentkezés gombra kattintás
send_login = driver.find_element(by=By.XPATH, value="/html/body/div/div/div/form/button")
send_login.click()
time.sleep(3)
driver.save_screenshot("successfully_login.png")
#-------------------------------------------------

#---- Statok lekérése a gombra kattintva ----
player_stats_button = driver.find_element(by=By.XPATH, value="/html/body/div/div/div/div/div[1]/div/button[2]")
player_stats_button.click()
time.sleep(1)
driver.save_screenshot("player_stats.png")
#-------------------------------------------------

#---- Leaderboard lekérése a gombra kattintva ----
leaderboard_button = driver.find_element(by=By.XPATH, value="/html/body/div/div/div/div/div[1]/div/button[3]")
leaderboard_button.click()
time.sleep(1)
driver.save_screenshot("leaderboard.png")
#-------------------------------------------------

#---- Settings lekérése a gombra kattintva ----
settings_button = driver.find_element(by=By.XPATH, value="/html/body/div/div/div/div/div[1]/div/button[4]")
settings_button.click()
time.sleep(1)
driver.save_screenshot("leaderboard.png")
#-------------------------------------------------

#---- Kijelentkezés ----
logout_button = driver.find_element(by=By.XPATH, value="/html/body/div/div/div/div/div[1]/div/button[1]")
logout_button.click()
time.sleep(1)
driver.save_screenshot("logout.png")
#-------------------------------------------------


# Kilépés a programból
driver.quit()




