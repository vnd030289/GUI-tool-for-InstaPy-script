using System;
using System.IO;
using System.Windows.Forms;

namespace InstaPy
{
	public partial class Form1 : Form
	{

		string FILENAME = "quickstart.py";
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			// Shows intro message to read all info / usage of the program
			MessageBox.Show("Please read info/usage before any program start.", "Read me", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void button1_Click(object sender, EventArgs e)
		{

		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Shows About page for program
			MessageBox.Show("GUI Tool for InstaPy script."+Environment.NewLine+Environment.NewLine + "Vesrion: 1.0" + Environment.NewLine + "Built in C#." + Environment.NewLine + "MIT License." + Environment.NewLine+Environment.NewLine +"InstaPy is utomation Script for \"farming\" Likes, Comments and Followers on Instagram." + Environment.NewLine + "Implemented in Python using the Selenium module." + Environment.NewLine + "MIT License.", "About",MessageBoxButtons.OK,MessageBoxIcon.Information);
		}

		private void button1_Click_1(object sender, EventArgs e)
		{

			// Import part of python file
			string import = "from instapy import InstaPy" + Environment.NewLine + "import os" + Environment.NewLine;
			File.WriteAllText(FILENAME, import);

			// Username and Password processing 
			string usernpass = "";
			if (username_txt.Text.Equals(string.Empty) || pass_txt.Text.Equals(string.Empty))
			{
				// Show error message if some of the field is empty
				MessageBox.Show("ERROR: Username or Password are empty !", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

				// Set focus to Username
				username_txt.Focus();
			}
			else
			{
				// If there is something in textboxes fill in line for sesion and login
				usernpass = "session = InstaPy(username='" + username_txt.Text + "', password='" + pass_txt.Text + "')" + Environment.NewLine + "session.login()" + Environment.NewLine;
				File.AppendAllText(FILENAME, usernpass);
			}

/*====================================================================================
*		Like Restriction Tags option
*			# searches the description for the given words and won't
*			# like the image if one of the words are in there
*			
*====================================================================================*/

				string[] likeRestrictionStrings = { };
				string likeRestrictionLine = "session.set_dont_like([";
						
				// Checks if option is selected
				if (likerestrict.Checked)
				{
				// If selected but no tags shows error message		

				if (likerestrict.Text.Equals(string.Empty)){

					MessageBox.Show("ERROR: No tags detected. Write some tags or deselect this option.", "ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);

					likerestrict.Focus();

				} else likeRestrictionStrings = likerestrict_txt.Text.Split(',');
				foreach (var item in likeRestrictionStrings)
				{
					// If there is emtpy tag skip it
					if (item.Equals(string.Empty))
					{
						continue;
					}else likeRestrictionLine += "'" + item + "', ";
				}
					// Close line with bracket
					likeRestrictionLine = likeRestrictionLine.Remove(likeRestrictionLine.Length-2,1)+"])"+Environment.NewLine;

					// Write in file
					File.AppendAllText(FILENAME,likeRestrictionLine);
				}

			/*====================================================================================
			*		Like Restriction Users option
			*			# searches the description for the given words and won't
			*			# like the image if one of the words are in there
			*			
			*====================================================================================*/

			string[] likeRestrictionUsersStrings = { };
			string likeRestrictionUsersLine = "session.set_ignore_users([";

			// Checks if option is selected
			if (restrictlikesusers.Checked)
			{
				// If selected but no tags shows error message		

				if (restrlikesusers.Text.Equals(string.Empty))
				{

					MessageBox.Show("ERROR: No people detected. Write some people or deselect this option.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

					restrlikesusers.Focus();

				}
				else likeRestrictionUsersStrings = restrlikesusers.Text.Split(',');
				foreach (var item in likeRestrictionUsersStrings)
				{
					// If there is emtpy tag skip it
					if (item.Equals(string.Empty))
					{
						continue;
					}
					else likeRestrictionUsersLine += "'" + item + "', ";
				}
				// Close line with bracket
				likeRestrictionUsersLine = likeRestrictionUsersLine.Remove(likeRestrictionUsersLine.Length - 2, 1) + "])" + Environment.NewLine;

				// Write in file
				File.AppendAllText(FILENAME, likeRestrictionUsersLine);
			}

			/*=======================================================================================
			*		Ignoring restriction python function
			*		Process it all tags in textbox or shows error if there is no tags
			*			#will ignore the don't like if the description contains
			*			# one of the given words
			*			
			*========================================================================================*/

			string[] ignoreRestrictionStrings = { };
			string ignoreRestrictionLine = "session.set_ignore_if_contains([";

			//if checked import is done and if not it will skip this block of code
			if (restrictignore.Checked) 
			{

				// if checked and there is no tags show error message to input some tags or deselect option
				if (restrictignore_txt.Text.Equals(string.Empty)) 
				{
					// MessageBox is shown
					MessageBox.Show("ERROR: No tags detected. Write some tags or deselect the option.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
					
					//Tags field gets in focus to type
					restrictignore_txt.Focus();
				}
				// if there is tags separated with comma ( , ) process it and add
				else ignoreRestrictionStrings = restrictignore_txt.Text.Split(','); 

				// Goes for every tag to add in string
				foreach (var item in ignoreRestrictionStrings)
				{
					// If there is empty tag caused with accident comma (ie. " dog, ") just continue
					if (item.Equals(string.Empty))
					{
						continue;
					}
					// If there is tag add it in line 
					else ignoreRestrictionLine += "'" + item + "', ";
				}

				// Removes processed comma to add closing bracket
				ignoreRestrictionLine = ignoreRestrictionLine.Remove(ignoreRestrictionLine.Length - 2, 1) + "])"+Environment.NewLine;

				File.AppendAllText(FILENAME, ignoreRestrictionLine);
			}

/*=================================================================================================
*		Friend exclusion 
*		# will prevent commenting on and unfollowing your good friends 
*		# (the images will still be liked)
* 
*=================================================================================================*/


			string[] friendExclusionStrings = { };
			string friendExclusionLine = "session.friend_list = [";

			// If Option is checked process everything
			if (friendexcl.Checked)
			{
				if (friendexcl_txt.Text.Equals(string.Empty))
				{
					// MessageBox is shown
					MessageBox.Show("ERROR: No friends detected. Write some friends or deselect the option.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

					//Tags field gets in focus to type
					friendexcl_txt.Focus();
				}
				else friendExclusionStrings = friendexcl_txt.Text.Split(',');
				// Goes for every tag to add in string
				foreach (var item in friendExclusionStrings)
				{
					// If there is empty tag caused with accident comma (ie. " dog, ") just continue
					if (item.Equals(string.Empty))
					{
						continue;
					}
					// If there is tag add it in line 
					else friendExclusionLine += "'" + item + "', ";
				}

				// Removes processed comma to add closing bracket
				friendExclusionLine = friendExclusionLine.Remove(friendExclusionLine.Length - 2, 1) + "]" + Environment.NewLine;

				File.AppendAllText(FILENAME, friendExclusionLine);
			}


			/*=============================================================================================
			 *			Commenting
			 *				Enables commenting 
			 *				Set percentage and custom comments
			 *=============================================================================================*/

			string commentLine = "session.set_do_comment(enabled=True, percentage=";
			string commentSetLine = "session.set_comments([";
			string[] comments = { };
			if (comment.Checked)
			{
				// Set percentage for commenting
				commentLine += comment_percent.Value.ToString() + ")" + Environment.NewLine;

				if (comment_cust_txt.Text.Equals(string.Empty))
				{
					// MessageBox is shown
					MessageBox.Show("ERROR: No comments detected. Write some comments or deselect the option.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

					// Comments field gets in focus to type
					comment_cust_txt.Focus();
				}
				else comments = comment_cust_txt.Text.Split(',');
				// Goes for every comment to add in string
				foreach (var item in comments)
				{
					// If there is empty comment caused with accident comma (ie. " nice, ") just continue
					if (item.Equals(string.Empty))
					{
						continue;
					}
					// If there is comment add it in line 
					else commentSetLine += "'" + item + "', ";
				}
				// Removes processed comma to add closing bracket
				commentSetLine = commentSetLine.Remove(commentSetLine.Length - 2, 1) + "])" + Environment.NewLine;

				File.AppendAllText(FILENAME, commentLine);
				File.AppendAllText(FILENAME, commentSetLine);
			}


			/*===============================================================================================
			 *			Following
			 *				Set percentage to follow every x/100th user
			 * 
			 * =============================================================================================*/
			if (following.Checked)
			{
				// Set following to true and read percent and times
				string follow = "session.set_do_follow(enabled=True, percentage=" 
					+ following_percent.Value.ToString() + ", times=" 
					+ followtimes.Value.ToString() + ")" + Environment.NewLine;

				File.AppendAllText(FILENAME, follow);
			}

			/*====================================================================================================
			 *			Unfollowing
			 *			#unfollows 10 of the accounts you're following -> instagram will only unfollow 10 before
			 *			you'll be 'blocked for 10 minutes'
			 *			
			 *			(if you enter a higher number than 10 it will unfollow 10,
			 *			then wait 10 minutes and will continue then)
			 *			
			 *======================================================================================================*/

			if (unfollow.Checked)
			{
				string unfollow = "session.unfollow_users(amount=" + unfollow_nmbr.Value.ToString() + ")" + Environment.NewLine;

				File.AppendAllText(FILENAME, unfollow);
			}

			/*=====================================================================================================
			 *			Interactions based on the number of followers a user has
			 *			
			 *			UPPER FOLLOWER COUNT -> This is used to check the number of followers a user has and
			 *				if this number exceeds the number set then no further interaction happens
			 *				
			 *			LOWER FOLLOWER COUNT -> #This is used to check the number of followers a user has and
			 *				if this number does not pass the number set then no further interaction happens
			 *				
			 *======================================================================================================*/

			if (upperfc.Checked)
			{
				string upper = "session.set_upper_follower_count(limit = " + upperfc_percent.Value.ToString() + ")" + Environment.NewLine;

				File.AppendAllText(FILENAME, upper);
			}

			if (lowerfc.Checked)
			{
				string lower = "session.set_lower_follower_count(limit = " + lowerfc_percent.Value.ToString() + ")" + Environment.NewLine;

				File.AppendAllText(FILENAME, lower);
			}


			/*========================================================================
			 *			Likes from tags
			 * 
			 * ========================================================================*/

			string likesFromTagsLine = "session.like_by_tags([";
			string[] tags = { };
			if (likesfromtags_txt.Text.Equals(string.Empty))
			{
				// MessageBox is shown
				MessageBox.Show("ERROR: No tags detected. Write some tags.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

				// Comments field gets in focus to type
				likesfromtags_txt.Focus();
			}
			else tags = likesfromtags_txt.Text.Split(',');

			// Goes for every tag to add in string
			foreach (var item in tags)
			{
				// If there is empty tag caused with accident comma (ie. " nice, ") just continue
				if (item.Equals(string.Empty))
				{
					continue;
				}
				// If there is tag add it in line 
				else likesFromTagsLine += "'" + item + "', ";
			}
			// Removes processed comma to add closing bracket
			likesFromTagsLine = likesFromTagsLine.Remove(likesFromTagsLine.Length - 2, 1)
				+ "], amount="+likes_nmbr.Value.ToString()+")" 
				+ Environment.NewLine + "session.end()";

			File.AppendAllText(FILENAME, likesFromTagsLine);

			File.WriteAllText("Start.bat", "set PYTHONIOENCODING=UTF-8"+Environment.NewLine+"py "+FILENAME);

			System.Diagnostics.Process.Start("Start.bat");
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void infoToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}
	}
}
